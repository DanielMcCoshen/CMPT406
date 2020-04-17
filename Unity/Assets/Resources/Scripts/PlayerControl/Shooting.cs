using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [Header("Object References")]
    public Rigidbody2D rb;
    public Camera mainCam;

    [Header("Item Usage")]
    private Vector3 smallMenuItemScale = new Vector3(.2f, .2f, 1f);
    private Vector3 normalMenuItemScale = new Vector3(.3f, .3f, 1f);
    public GameObject itemEquipMenu;
    private GameObject[] itemEquipSymbols = new GameObject[3];
    private bool deactivateItemMenu = false;
    private float itemDeactivationTime = 0f;
    public int equippedItemIndex;
    private List<string> itemNames = new List<string>();
    public Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();
    public GameObject itemEquipped;

    [Header("Weapon Firing")]
    public Transform firePoint;
    public Transform aimPoint;
    public List<GameObject> weapons;

    [Header("Equip Weapon Menu")]
    public GameObject weaponEquipMenu;
    private GameObject[] weaponEquipSymbols = new GameObject[3];
    private bool deactivateMenu = false;
    private float deactivationTime = 0f;
    public GameObject weaponEquipped = null;
    private int equippedWeaponIndex = 0;
    private static Vector3 menuWeaponScale = new Vector3(0.4118056f, 1.235417f, 1.235417f);
    private static Vector3 menuSmallWeaponScale = new Vector3(0.2676736f, 0.803021f, 1.235417f);
    public AudioSource equipEquipmentSFX;

    [Header("Misc")]
    public OnDeathTrapEnterPlayer playersDeathTrap;

    public bool useController;

    public PlayerAnimations playerAnimationManager;

    Vector2 mousePos;
    Vector2 controllerPos;

    /**
     * Used for collecting weapon and item pickups when they enter the collider on the object this component
     * is attached to.
     */
    void OnTriggerEnter2D(Collider2D col)
    {
        void ReceiveWeapon()
        {
            GameObject obj = Instantiate(col.gameObject.GetComponent<ItemPickup>().GetItem(), firePoint.position,
                firePoint.rotation, firePoint);

            weapons.Add(obj.gameObject);
            col.gameObject.GetComponent<ItemPickup>().DestroySelf();
            
            obj.SetActive(false);
            EquipWeapon(weapons.Count-1);
        }

        void ReceiveItem()
        {
            void AddItem(GameObject obj, string name)
            {
                itemNames.Add(name);
                obj.GetComponent<UsableItems>().SetUpItem(gameObject);
                items.Add(name, obj.gameObject);
                col.gameObject.GetComponent<ItemPickup>().DestroySelf();
                obj.SetActive(false);
                if (!HasItemEquipped())
                {
                    EquipItem(itemNames.Count-1);
                }
            }

            GameObject objt = Instantiate(col.gameObject.GetComponent<ItemPickup>().GetItem(), firePoint.position,
                firePoint.rotation, firePoint);
            string nam = objt.gameObject.GetComponent<UsableItems>().GetItemType();

            if (items.ContainsKey(nam))
            {
                int usesToAdd = objt.GetComponent<UsableItems>().GetNumberOfUses();
                items[nam].GetComponent<UsableItems>().AddItemUsages(usesToAdd);
                col.gameObject.GetComponent<ItemPickup>().DestroySelf();
                Destroy(objt);
            }
            else
            {
                AddItem(objt, nam);
            }
            
        }

        if (col.gameObject.tag == "Weapon")
        {
            ReceiveWeapon();
        }
        else if(col.gameObject.tag == "Souls")
        {
            playersDeathTrap.AddLife();
            col.gameObject.GetComponent<ItemPickup>().DestroySelf();
        }
        else if(col.gameObject.tag == "Item")
        {
            ReceiveItem();
        }
    }

    void Start()
    {
        weaponEquipMenu.SetActive(false);
        if(weapons.Count > 0)
        {
            EquipWeapon(0);
            foreach (GameObject weapon in weapons)
            {
                weapon.GetComponent<Weapon>().SetAimPoint(firePoint);
            }
            for(int index = 1; index < weapons.Count; index++)
            {
                weapons[index].SetActive(false);
            }
        }
    }

    private int[] GetSurroundingIndices(int index, int size)
    {
        int prev = index - 1;
        int next = index + 1;
        if (prev == -1)
        {
            prev = size - 1;
        }
        if (next == size)
        {
            next = 0;
        }
        return new int[] { prev, next };
    }

    private void UpdateItemEquipMenu()
    {
        void InstantiateMenuSymbols(int prev, int next, Transform menuTransform, Vector3 menuPosition)
        {
            itemEquipSymbols[0] = Instantiate(items[itemNames[prev]].GetComponent<UsableItems>().GetItemMenuPrefab(),
            new Vector3(menuPosition.x - 0.75f, menuPosition.y, menuPosition.z), Quaternion.identity, menuTransform);

            itemEquipSymbols[1] = Instantiate(items[itemNames[equippedItemIndex]].GetComponent<UsableItems>().GetItemMenuPrefab(),
                new Vector3(menuPosition.x, menuPosition.y, menuPosition.z), Quaternion.identity, menuTransform);

            itemEquipSymbols[2] = Instantiate(items[itemNames[next]].GetComponent<UsableItems>().GetItemMenuPrefab(),
                new Vector3(menuPosition.x + 0.75f, menuPosition.y, menuPosition.z), Quaternion.identity, menuTransform);

            itemEquipSymbols[0].transform.localScale = smallMenuItemScale;
            itemEquipSymbols[1].transform.localScale = normalMenuItemScale;
            itemEquipSymbols[2].transform.localScale = smallMenuItemScale;
        }

        void DestroyOldItemIcons()
        {
            foreach (GameObject item in itemEquipSymbols)
            {
                if (item != null && !item.Equals(null))
                {
                    Destroy(item);
                }
            }
        }

        void SetupItemMenu()
        {
            deactivateItemMenu = true;
            itemDeactivationTime = Time.time + 3f;
            itemEquipMenu.SetActive(true);
            Transform menuTransform = itemEquipMenu.transform;
            Vector3 menuPosition = menuTransform.position;

            int[] indices = GetSurroundingIndices(equippedItemIndex,itemNames.Count);
            InstantiateMenuSymbols(indices[0], indices[1], menuTransform, menuPosition);
        }

        DestroyOldItemIcons();
        SetupItemMenu();

    }

    private void UpdateWeaponEquipMenu()
    {
        void InstantiateMenuSymbols(int prev, int next, Transform menuTransform, Vector3 menuPosition)
        {
            weaponEquipSymbols[0] = Instantiate(weapons[prev].GetComponent<Weapon>().GetWeaponMenuPrefab(),
            new Vector3(menuPosition.x - 0.75f, menuPosition.y, menuPosition.z), Quaternion.identity, menuTransform);

            weaponEquipSymbols[1] = Instantiate(weapons[equippedWeaponIndex].GetComponent<Weapon>().GetWeaponMenuPrefab(),
                new Vector3(menuPosition.x, menuPosition.y, menuPosition.z), Quaternion.identity, menuTransform);

            weaponEquipSymbols[2] = Instantiate(weapons[next].GetComponent<Weapon>().GetWeaponMenuPrefab(),
                new Vector3(menuPosition.x + 0.75f, menuPosition.y, menuPosition.z), Quaternion.identity, menuTransform);

            weaponEquipSymbols[0].transform.localScale = menuSmallWeaponScale;
            weaponEquipSymbols[1].transform.localScale = menuWeaponScale;
            weaponEquipSymbols[2].transform.localScale = menuSmallWeaponScale;
        }

        void DestroyOldWeaponIcons()
        {
            foreach (GameObject weapon in weaponEquipSymbols)
            {
                if (weapon != null && !weapon.Equals(null))
                {
                    Destroy(weapon);
                }
            }
        }

        void SetupWeaponMenu()
        {
            deactivateMenu = true;
            deactivationTime = Time.time + 3f;
            weaponEquipMenu.SetActive(true);
            Transform menuTransform = weaponEquipMenu.transform;
            Vector3 menuPosition = menuTransform.position;

            int[] indices = GetSurroundingIndices(equippedWeaponIndex, weapons.Count);
            InstantiateMenuSymbols(indices[0], indices[1], menuTransform, menuPosition);
        }

        DestroyOldWeaponIcons();
        SetupWeaponMenu();
        
    }

    public bool HasItemEquipped()
    {
        return itemEquipped != null && !itemEquipped.Equals(null);
    }

    private void EquipItem(int indx)
    {
        void ChangeItem(int index)
        {
            equipEquipmentSFX.Play();
            itemEquipped = items[itemNames[index]];
            itemEquipped.SetActive(true);
            equippedItemIndex = index;
        }
        if (HasItemEquipped())
        {
            itemEquipped.SetActive(false);
        }
        ChangeItem(indx);
        UpdateItemEquipMenu();
    }

    private void EquipWeapon(int indx)
    {
        void ChangeWeapon(int index)
        {
            equipEquipmentSFX.Play();
            weaponEquipped = weapons[index];
            weaponEquipped.SetActive(true);
            weaponEquipped.GetComponent<PlayerWeaponAnimations>().Equipped(playerAnimationManager.direction);
            equippedWeaponIndex = index;
        }

        if (HasWeaponEquipped())
        {
            weaponEquipped.SetActive(false);
        }
        ChangeWeapon(indx);
        UpdateWeaponEquipMenu();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            UseItem();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EquipWeapon(GetSurroundingIndices(equippedWeaponIndex, weapons.Count)[0]);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            EquipWeapon(GetSurroundingIndices(equippedWeaponIndex, weapons.Count)[1]);
        }
        else
        {
            for (int index = 0; index < weapons.Count; index++)
            {
                if (Input.GetKeyDown("" + (index + 1)))
                {
                    EquipWeapon(index);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(itemNames.Count > 0)
            {
                EquipItem(GetSurroundingIndices(equippedItemIndex, itemNames.Count)[0]);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (itemNames.Count > 0)
            {
                EquipItem(GetSurroundingIndices(equippedItemIndex, itemNames.Count)[1]);
            }
        }

        void GetMouseInput()
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }

        GetMouseInput();
        if (deactivateMenu)
        {
            if (deactivationTime < Time.time)
            {
                deactivateMenu = false;
                foreach(GameObject weapon in weaponEquipSymbols)
                {
                    Destroy(weapon);
                }
                weaponEquipMenu.SetActive(false);
            }
        }
        if (deactivateItemMenu)
        {
            if(itemDeactivationTime < Time.time)
            {
                deactivateItemMenu = false;
                Destroy(itemEquipSymbols[0]);
                Destroy(itemEquipSymbols[2]);
            }
        }
    }

    void FixedUpdate()
    {
        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        void RotateWithMouse()
        {
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(aimPoint.position);

            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

            aimPoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle+90f));

        }

        void RotateWithController()
        {
            if (controllerPos.sqrMagnitude > 0.0f)
            {
                
            }
        }

        if (!useController)
        {
            RotateWithMouse();
        }

        if (useController)
        {
            RotateWithController();
        }
    }

    void UseItem()
    {
        if(HasItemEquipped())
        {
            itemEquipped.GetComponent<UsableItems>().UseItem();
        }
    }

    public void ItemUsedUp(string itemName)
    {
        itemNames.Remove(itemName);
        Destroy(items[itemName]);
        items[itemName] = null;
        items.Remove(itemName);
    }

    void Shoot()
    {
        if (HasWeaponEquipped())
        {
           weaponEquipped.GetComponent<Weapon>().FireWeapon();
        }
    }

    public GameObject CurrentWeapon()
    {
        return weaponEquipped;
    }

    public bool HasWeaponEquipped()
    {
        return (weaponEquipped != null && !weaponEquipped.Equals(null));
    }

    public Transform GetFirePoint()
    {
        return firePoint;
    }

    public Transform GetAimPoint()
    {
        return aimPoint;
    }
}
