using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    // !!!! This is a stripped down version of my 306's turret script. I think it's a good place to start.

    //[SerializeField]
    private Transform target;

    [Header("Weapon Settings")]

    public Transform firePoint;

    [Header("Bullet Settings")]

    [SerializeField]
    [Tooltip("How many shots a second.")]
    private float fireRate = 1f;
    private float fireCountdown = 0f;
    [SerializeField]
    public GameObject bulletPrefab;

    [Header("Burst Fire Settings")]
    [SerializeField]
    private bool doBurstFire = false;
    [SerializeField]
    private float burstFireRate = 0.5f;
    [SerializeField]
    private float burstSize = 3f;

    [Header("Beam Settings")]
    [SerializeField]
    private bool useLaser = false;

    [SerializeField]
    private int damageOverTime = 30;
    [Tooltip("The enemy's speed will be reduced by this percentage.")]
    public float slowPercentage = 0.5f;

    [SerializeField]
    public LineRenderer lineRenderer;
    [SerializeField]
    public ParticleSystem impactEffect;
    [SerializeField]
    public Light impactLight;

    [Header("Radiator Settings")]
    [SerializeField]
    private bool doRadiation = false;

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
        Bullet bullet = bulletRef.GetComponent<Bullet>();

        if (bullet != null)
        {
            //bullet.SelectTarget(target);
        }
    }
}
