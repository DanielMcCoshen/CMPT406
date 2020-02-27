using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AimAtPlayer : MonoBehaviour
{

    public Transform playerPosition = null;
    public Transform aimPoint;
    public Transform prefabParent;
    public Transform firePoint;

    public void FacePlayer()
    {
        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
        
        Vector2 playerPos = new Vector2(playerPosition.position.x, playerPosition.position.y);

        Vector2 aimPos = new Vector2(prefabParent.position.x, prefabParent.position.y + aimPoint.position.y);

        Vector2 firePointPos = new Vector2(prefabParent.position.x + firePoint.position.x, prefabParent.position.y + firePoint.position.y);

        Vector2 cDifference = (playerPos - aimPos);

        Vector2 aDifference = (firePointPos - aimPos);

        double cx = 0;
        if(cDifference.x != 0)
        {
            cx = Math.Pow(cDifference.x, 2.0);
        }
        double cy = 0;
        if (cDifference.y != 0)
        {
            cy = Math.Pow(cDifference.y, 2.0);
        }

        double cMag = 0;
        if (cx + cy != 0)
        {
            cMag = Math.Sqrt(cx + cy);
        }

        double ax = 0;
        if (aDifference.x != 0)
        {
            ax = Math.Pow(aDifference.x, 2.0);
        }

        double ay = 0;
        if (aDifference.y != 0)
        {
            ay = Math.Pow(aDifference.y, 2.0);
        }

        double aMag = 0;
        if (ax + ay != 0)
        {
            aMag = Math.Sqrt(ax + ay);
        }

        double addition = 0;
        //Debug.Log("Amag " + aMag + " cmag " + cMag);

        if (aMag != 0 && cMag != 0 && aMag/cMag <1)
        {
            addition = Math.Asin((aMag / cMag));
        }

        //Debug.Log(addition);
        double angle =  addition + AngleBetweenTwoPoints(aimPos, playerPos);
        aimPoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, (float)(angle + 90f)));
    }
    
    void Update()
    {
        if (playerPosition != null && !playerPosition.Equals(null))
        {
            FacePlayer();
        }
        
    }
}
