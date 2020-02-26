using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSetup : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private BezierRoute route;
    [SerializeField]
    private BezierFollow projectile;

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
