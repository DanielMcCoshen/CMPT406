using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    // !!!! This is a stripped down version of my 306's turret script. I think it's a good place to start.

    //[SerializeField]
    private Transform target = null;

    [Header("Weapon Settings")]

    public Transform firePoint;

    [SerializeField]
    public GameObject bulletPrefab;


    // Private References
    private GameObject rangeIndicator;
    private Transform partToRotate;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * Point the turret at the target.
     */
    void PointToTarget()
    {
        // Point toward target
        Vector3 direction = target.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // !!! Okay, we wouldn't actually want a slerp.
        //partToRotate.rotation = Quaternion.Slerp(partToRotate.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    /**
     * Shoot.
     */
    void Shoot()
    {
        GameObject bulletRef = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Weapon bullet = bulletRef.GetComponent<Weapon>();

        if (bullet != null)
        {
            //bullet.SelectTarget(target);
        }
    }
}
