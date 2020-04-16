using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeShell : MonoBehaviour
{
    public float rotationRate = 10.5f;
    public int numberOfProjectiles = 17;
    public GameObject projectilePrefab;

    public void FireProjectile(Vector3 position, Quaternion rotation)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector3 direction = new Vector3(projectile.transform.up.x, projectile.transform.up.y * .5f, projectile.transform.up.z);
        rb.AddForce(direction * 5f, ForceMode2D.Impulse);
    }

    public void FireInASpread()
    {
        for (int variance = -(numberOfProjectiles - 1); variance < numberOfProjectiles; variance++)
        {
            FireProjectile(gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, (rotationRate * (float)variance) + gameObject.transform.rotation.eulerAngles.z)));
        }
    }

    void Start()
    {
        StartCoroutine(DestroyAndSpawnProjectiles());
    }

    IEnumerator DestroyAndSpawnProjectiles()
    {
        yield return new WaitForSeconds(.5f);
        FireInASpread();
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }
}
