using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public List<GameObject> weapons;

    public GameObject weaponEquipped = null;

    void Start()
    {
        if(weapons.Count > 0)
        {
            weaponEquipped = weapons[0];
            foreach (GameObject weapon in weapons)
            {
                weapon.GetComponent<Weapon>().SetFirePoint(firePoint);
            }
        }
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
                if(weaponEquipped != null && !weaponEquipped.Equals(null))
                {
                    weaponEquipped.SetActive(false);
                }
                
                weaponEquipped = weapons[index];
                weaponEquipped.SetActive(true);
            }
        }
    }
    
    void Shoot()
    {
        if (weaponEquipped != null && !weaponEquipped.Equals(null))
        {
           weaponEquipped.GetComponent<Weapon>().FireWeapon();
        }
    }
}
