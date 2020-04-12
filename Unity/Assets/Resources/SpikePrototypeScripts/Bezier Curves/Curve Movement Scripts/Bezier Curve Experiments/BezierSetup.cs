using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSetup : MonoBehaviour
{
    [SerializeField]
    public Transform firePoint;
    [SerializeField]
    public Transform target;
    [SerializeField]
    public BezierRoute route;
    [SerializeField]
    public BezierFollow projectile;

    // Start is called before the first frame update
    void Start()
    {
        route.SetStartAndEnd(firePoint, target);
        projectile.StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
