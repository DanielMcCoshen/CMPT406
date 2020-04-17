using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : EntityAnimation
{
    public GameObject[] directionalObjects;
    public Animator[] directionalAnimators;
    public int direction = 0;
    public int currentMovementDirection = -1;

    public Shooting weaponManager;

    void Start()
    {
        directionalAnimators[0].SetBool("BeingUsed", true);
        for (int index = 1; index < 4; index++)
        {
            directionalAnimators[index].SetBool("BeingUsed", false);
        }
    }
    
    public override void FaceNorth()
    {
        directionalAnimators[direction].SetBool("BeingUsed", false);
        directionalAnimators[0].SetBool("BeingUsed", true);
        
        direction = 0;
        if (weaponManager.HasWeaponEquipped())
        {
            weaponManager.CurrentWeapon().GetComponent<PlayerWeaponAnimations>().FaceNorth();
        }
    }

    public override void FaceEast()
    {
        directionalAnimators[direction].SetBool("BeingUsed", false);
        directionalAnimators[1].SetBool("BeingUsed", true);
        
        direction = 1;
        if (weaponManager.HasWeaponEquipped())
        {
            weaponManager.CurrentWeapon().GetComponent<PlayerWeaponAnimations>().FaceEast();
        }
    }

    public override void FaceSouth()
    {
        directionalAnimators[direction].SetBool("BeingUsed", false);

        directionalAnimators[2].SetBool("BeingUsed", true);

        
        direction = 2;
        if (weaponManager.HasWeaponEquipped())
        {
            weaponManager.CurrentWeapon().GetComponent<PlayerWeaponAnimations>().FaceSouth();
        }
    }

    public override void FaceWest()
    {
        directionalAnimators[direction].SetBool("BeingUsed", false);

        directionalAnimators[3].SetBool("BeingUsed", true);
        
        direction = 3;
        if (weaponManager.HasWeaponEquipped())
        {
            weaponManager.CurrentWeapon().GetComponent<PlayerWeaponAnimations>().FaceWest();
        }
    }

    public override void CheckRotation()
    {
        if (((aimPoint.rotation.eulerAngles.z >= 0 && aimPoint.rotation.eulerAngles.z <= 45) 
            || (aimPoint.rotation.eulerAngles.z > 315 && aimPoint.rotation.eulerAngles.z < 360)) && direction != 0)
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

    private int UpdateMovementDirection()
    {
        if (Input.GetAxis("Horizontal") < Input.GetAxis("Vertical") && 0 < Input.GetAxis("Vertical"))
        {
            return 0;
        }
        else if (Input.GetAxis("Horizontal") > Input.GetAxis("Vertical") && 0 < Input.GetAxis("Horizontal"))
        {
            return 1;
        }
        else if (Input.GetAxis("Horizontal") > Input.GetAxis("Vertical") && 0 > Input.GetAxis("Vertical"))
        {
            return 2;
        }
        else if (Input.GetAxis("Horizontal") < Input.GetAxis("Vertical") && 0 > Input.GetAxis("Horizontal"))
        {
            return 3;
        }
        else
        {
            return -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
        if(directionalObjects[direction].active)
        {
            int dir = UpdateMovementDirection();
            directionalAnimators[direction].SetInteger("CurrentDirection", dir);
        }

    }
}
