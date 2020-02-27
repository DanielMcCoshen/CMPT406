using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadWeapon : Weapon
{

    public int numberOfProjectiles;
    public float rotationRate;

    public void FireInASpread()
    {
        for (int variance = -(numberOfProjectiles-1); variance < numberOfProjectiles; variance++)
        {
            FireProjectile(firePoint.position, Quaternion.Euler(new Vector3(0, 0, (rotationRate * (float)variance) + aimPoint.rotation.eulerAngles.z)));
        }
    }

    public override void FireWeapon()
    {
        if (!onCooldown)
        {
            FireInASpread();
        }
    }
}
