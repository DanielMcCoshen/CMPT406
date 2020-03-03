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
        double MagCalculation(Vector2 difference)
        {
            double x = 0;
            if (difference.x != 0)
            {
                x = Math.Pow(difference.x, 2.0);
            }

            double y = 0;
            if (difference.y != 0)
            {
                y = Math.Pow(difference.y, 2.0);
            }

            double mag = 0;
            if (x + y != 0)
            {
                mag = Math.Sqrt(x + y);
            }
            return mag;
        }

        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
        
        Vector2 playerPos = new Vector2(playerPosition.position.x, playerPosition.position.y);

        Vector2 aimPos = new Vector2(aimPoint.position.x, aimPoint.position.y);

        Vector2 firePointPos = new Vector2( firePoint.position.x,  firePoint.position.y);

        Vector2 cDifference = (playerPos - aimPos);

        Vector2 aDifference = (firePointPos - aimPos);

        double aMag = MagCalculation(aDifference);

        double cMag = MagCalculation(cDifference);

        double addition = 0;
        //Debug.Log("Amag " + aMag + " cmag " + cMag);

        if (aMag != 0 && cMag != 0 && aMag/cMag <=1)
        {
            addition = Math.Asin((aMag / cMag));
        }

        //Debug.Log(addition);
        double angle =  addition + AngleBetweenTwoPoints(aimPos, playerPos);
        aimPoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, (float)(angle + 90f)));
    }
    
    void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (playerPosition != null && !playerPosition.Equals(null))
        {
            FacePlayer();
        }
        
    }
}
