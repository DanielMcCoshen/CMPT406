using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : UsableItems
{
    public float rotationRate = 10.5f;
    public int numberOfProjectiles = 17;
    public GameObject grenadeShellPrefab;
    public AudioSource throwSFX;

    public void ThrowGrenade(Vector3 position, Quaternion rotation)
    {
        throwSFX.Play();
        GameObject projectile = Instantiate(grenadeShellPrefab, position, rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector3 direction = new Vector3(projectile.transform.up.x, projectile.transform.up.y * .5f, projectile.transform.up.z);
        rb.AddForce(direction * 8f, ForceMode2D.Impulse);
    }

    public override void ItemActivation()
    {
        ThrowGrenade(firePoint.position, aimPoint.rotation);
    }

}
