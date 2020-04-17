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
    public Transform aimPoint = null;
    public int firesBeforeCooldown;
    public int numberOfShotsFired = 0;
    public GameObject weaponMenuPrefab;
    // Deletes bullet if it touches a Collider2D that is not the player.
    public AudioSource firingSFX;

    public void SetFirePoint(Transform fp)
    {
        firePoint = fp;
    }

    public void SetAimPoint(Transform fp)
    {
        aimPoint = fp;
    }

    public virtual void FireProjectile(Vector3 position, Quaternion rotation)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation );
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector3 direction = new Vector3(projectile.transform.up.x, projectile.transform.up.y * .5f, projectile.transform.up.z);
        rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
    }

    public virtual void FireWeapon()
    {
        if (!onCooldown)
        {
            firingSFX.Play();
            FireProjectile(firePoint.position, aimPoint.rotation);
            StartCoroutine(WeaponCooldown());
        }
        
    }

    public GameObject GetWeaponMenuPrefab()
    {
        return weaponMenuPrefab;
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
