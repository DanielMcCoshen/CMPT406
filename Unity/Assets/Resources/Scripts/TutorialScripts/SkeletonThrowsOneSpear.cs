using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonThrowsOneSpear : MonoBehaviour
{
    public Transform aimPoint;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileForce;
    private bool hasntThrownSpear = true;

    public void ThrowSpear(Vector3 position, Quaternion rotation)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Debug.Log(rb);
        rb.AddForce(projectile.transform.up * -projectileForce, ForceMode2D.Impulse);
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (hasntThrownSpear)
        {
            hasntThrownSpear = false;
            StartCoroutine(SpearThrow());
        }
    }
    IEnumerator SpearThrow()
    {
        yield return new WaitForSeconds(1f);
        ThrowSpear(firePoint.position, aimPoint.rotation);
    }
}
