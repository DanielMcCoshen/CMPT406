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
        for(int index = 1; index < 4; index++)
        {
            directionalObjects[index].SetActive(false);
        }
    }

    
    public override void FaceNorth()
    {
        directionalObjects[0].SetActive(true);
        directionalObjects[direction].SetActive(false);
        direction = 0;
        if (weaponManager.HasWeaponEquipped())
        {
            weaponManager.CurrentWeapon().GetComponent<PlayerWeaponAnimations>().FaceNorth();
        }
        directionalAnimators[0].CrossFade("NorthFacingIdle", 0.1f, -1, 0.0f);
    }

    public override void FaceEast()
    {
        directionalObjects[1].SetActive(true);
        directionalObjects[direction].SetActive(false);
        direction = 1;
        if (weaponManager.HasWeaponEquipped())
        {
            weaponManager.CurrentWeapon().GetComponent<PlayerWeaponAnimations>().FaceEast();
        }
        directionalAnimators[1].CrossFade("EastFacingIdle", 0.1f, -1, 0.0f);
    }

    public override void FaceSouth()
    {
        
        directionalObjects[2].SetActive(true);
        directionalObjects[direction].SetActive(false);
        direction = 2;
        if (weaponManager.HasWeaponEquipped())
        {
            weaponManager.CurrentWeapon().GetComponent<PlayerWeaponAnimations>().FaceSouth();
        }
        directionalAnimators[2].CrossFade("SouthFacingIdle", 0.1f, -1, 0.0f);
    }

    public override void FaceWest()
    {
        
        directionalObjects[3].SetActive(true);
        directionalObjects[direction].SetActive(false);
        direction = 3;
        if (weaponManager.HasWeaponEquipped())
        {
            weaponManager.CurrentWeapon().GetComponent<PlayerWeaponAnimations>().FaceWest();
        }
        directionalAnimators[3].CrossFade("WestFacingIdle", 0.1f, -1, 0.0f);
    }

    public override void CheckRotation()
    {
        if (((aimPoint.rotation.eulerAngles.z >= 0 && aimPoint.rotation.eulerAngles.z <= 45) 
            || (aimPoint.rotation.eulerAngles.z > 315 && aimPoint.rotation.eulerAngles.z < 360)) && direction != 0)
        {
            Debug.Log(direction);
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
            Debug.Log(dir);
            directionalAnimators[direction].SetInteger("CurrentDirection", dir);
        }

    }
}
