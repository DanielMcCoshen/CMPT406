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

    public GameObject weaponEquipped = null;

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
        }
    }

    void Start()
    {
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

    private void EquipWeapon(int index)
    {
        if (HasWeaponEquipped())
        {
            weaponEquipped.SetActive(false);
        }

        weaponEquipped = weapons[index];
        weaponEquipped.SetActive(true);
        weaponEquipped.GetComponent<PlayerWeaponAnimations>().Equipped(playerAnimationManager.direction);
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
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
    }

    void FixedUpdate()
    {
        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        void RotateWithMouse()
        {

            //Get the Screen positions of the object
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(aimPoint.position);

            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

            //Ta Daaa
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
