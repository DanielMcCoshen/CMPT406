using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileForce = 20f;
    public float cooldownTimer;
    public bool onCooldown = false;
    public Transform firePoint = null;
    public int firesBeforeCooldown;
    public int numberOfShotsFired = 0;
    // Deletes bullet if it touches a Collider2D that is not the player.

    public void SetFirePoint(Transform fp)
    {
        firePoint = fp;
    }

    public void FireProjectile(Vector3 position, Quaternion rotation)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation );
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(projectile.transform.up * projectileForce, ForceMode2D.Impulse);
    }

    public virtual void FireWeapon()
    {
        if (!onCooldown)
        {
            FireProjectile(firePoint.position, firePoint.rotation);
            StartCoroutine(WeaponCooldown());
        }
        
    }

    void OnEnable()
    {
        onCooldown = false;
        numberOfShotsFired = 0;
    }

    public IEnumerator WeaponCooldown()
    {
        if (numberOfShotsFired+1 >= firesBeforeCooldown)
        {
            onCooldown = true;
            yield return new WaitForSeconds(cooldownTimer);
            onCooldown = false;
            numberOfShotsFired = 0;
        }
        else
        {
            numberOfShotsFired++;
        }
    }
}
