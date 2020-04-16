using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDirectionEnemyAnimations : EntityAnimations
{

    public GameObject[] directionalObjects;
    public int direction = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int index = 1; index < 4; index++)
        {
            directionalObjects[index].SetActive(false);
        }
    }

    public override void FaceNorth()
    {
        directionalObjects[0].SetActive(true);
        directionalObjects[direction].SetActive(false);
        direction = 0;

    }

    public override void FaceEast()
    {
        directionalObjects[1].SetActive(true);
        directionalObjects[direction].SetActive(false);
        direction = 1;
    }

    public override void FaceSouth()
    {
        directionalObjects[2].SetActive(true);
        directionalObjects[direction].SetActive(false);
        direction = 2;
    }

    public override void FaceWest()
    {
        directionalObjects[3].SetActive(true);
        directionalObjects[direction].SetActive(false);
        direction = 3;
    }

    public override void CheckRotation()
    {
        if (((aimPoint.rotation.eulerAngles.z >= 0 && aimPoint.rotation.eulerAngles.z <= 45) || (aimPoint.rotation.eulerAngles.z > 315 && aimPoint.rotation.eulerAngles.z < 360)) && direction != 0)
        {
            FaceNorth();
        }
        else if (aimPoint.rotation.eulerAngles.z > 225 && aimPoint.rotation.eulerAngles.z <= 315 && direction != 1)
        {
            FaceEast();
        }
        else if (aimPoint.rotation.eulerAngles.z > 135 && aimPoint.rotation.eulerAngles.z <= 225 && direction != 2)
        {
            FaceSouth();
        }
        else if (aimPoint.rotation.eulerAngles.z > 45 && aimPoint.rotation.eulerAngles.z <= 135 && direction != 3)
        {
            FaceWest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
    }
}
