using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimation : MonoBehaviour
{
    public Transform aimPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void FaceSouth()
    {
        Debug.Log("Facing south");
    }
    public virtual void FaceNorth()
    {
        Debug.Log("Facing north");
    }
    public virtual void FaceEast()
    {
        Debug.Log("Facing east");
    }
    public virtual void FaceWest()
    {
        Debug.Log("Facing west");
    }

    public virtual void CheckRotation()
    {
        if (aimPoint.rotation.eulerAngles.z > -45 && aimPoint.rotation.eulerAngles.z <= 45)
        {
            FaceNorth();
        }
        else if (aimPoint.rotation.eulerAngles.z > 45 && aimPoint.rotation.eulerAngles.z <= 135)
        {
            FaceWest();
        }
        else if (aimPoint.rotation.eulerAngles.z > 135 && aimPoint.rotation.eulerAngles.z <= 225)
        {
            FaceSouth();
        }
        else if (aimPoint.rotation.eulerAngles.z > 225 && aimPoint.rotation.eulerAngles.z <= 315)
        {
            FaceEast();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
    }
}
