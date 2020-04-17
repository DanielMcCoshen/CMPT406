using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShooting : Shooting
{
    private bool mayShoot = false;
    private bool mayUseItems = false;

    public override void UseItem()
    {
        if (HasItemEquipped() && mayUseItems)
        {
            itemEquipped.GetComponent<UsableItems>().UseItem();
        }
    }

    public override void Shoot()
    {
        if (HasWeaponEquipped() && mayShoot)
        {
            weaponEquipped.GetComponent<Weapon>().FireWeapon();
        }
    }

    public void SetIfPlayerCanShoot(bool isShootingAllowed)
    {
        mayShoot = isShootingAllowed;
    }

    public void SetIfPlayerCanUseItems(bool isUsingItemsAllowed)
    {
        mayUseItems = isUsingItemsAllowed;
    }

    public bool HasGrenades()
    {
        if (items.ContainsKey("Grenade"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
