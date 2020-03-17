using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [Header("Object References")]
    public Rigidbody2D rb;
    public Camera mainCam;

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

    [Header("Misc")]
    public OnDeathTrapEnterPlayer playersDeathTrap;

    public bool useController;

    public PlayerAnimations playerAnimationManager;

    Vector2 mousePos;
    Vector2 controllerPos;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Weapon")
        {
            GameObject obj = Instantiate(col.gameObject.GetComponent<ItemPickup>().GetItem(), firePoint.position, 
                firePoint.rotation, firePoint);
            weapons.Add(obj.gameObject);
            col.gameObject.GetComponent<ItemPickup>().DestroySelf();
            obj.SetActive(false);
        }
        else if(col.gameObject.tag == "Souls")
        {
            playersDeathTrap.AddLife();
            col.gameObject.GetComponent<ItemPickup>().DestroySelf();
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

        int[] GetIndices()
        {
            int prev = equippedWeaponIndex - 1;
            int next = equippedWeaponIndex + 1;
            if (prev == -1)
            {
                prev = weapons.Count - 1;
            }
            if (next == weapons.Count)
            {
                next = 0;
            }
            return new int[] { prev, next };
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

            int[] indices = GetIndices();
            InstantiateMenuSymbols(indices[0], indices[1], menuTransform, menuPosition);
        }

        DestroyOldWeaponIcons();
        SetupWeaponMenu();
        
    }

    private void EquipWeapon(int indx)
    {
        void ChangeWeapon(int index)
        {
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

        if (GetMouseInput.GetButtonDown("G"))
        {

        }

        for(int index = 0; index < weapons.Count; index++)
        {
            if(Input.GetKeyDown("" + (index+1)))
            {
                EquipWeapon(index);
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
}
