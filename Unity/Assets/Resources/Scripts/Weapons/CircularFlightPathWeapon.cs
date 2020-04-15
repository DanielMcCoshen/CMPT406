using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularFlightPathWeapon : Weapon
{
    public override void FireProjectile(Vector3 position, Quaternion rotation)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector3 direction = new Vector3(projectile.transform.up.x, projectile.transform.up.y * .5f, projectile.transform.up.z);
        projectile.GetComponent<CircularPathProjectile>().GiveDirection(direction);
    }
}
