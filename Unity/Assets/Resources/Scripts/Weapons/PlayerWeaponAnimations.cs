using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAnimations : EntityAnimations
{

    public GameObject northSouthSprite;
    public GameObject eastWestSprite;
    protected int currentDirection;
    public Weapon weapon;
    public Transform northSouthFirePoint;
    public Transform eastWestFirePoint;

    public virtual void Equipped(int direction)
    {
        if(direction == 0)
        {
            currentDirection = 1;
            FaceNorth();
        }
        else if(direction == 1)
        {
            currentDirection = 0;
            FaceEast();
        }
        else if (direction == 2)
        {
            currentDirection = 1;
            FaceSouth();
        }
        else if (direction == 3)
        {
            currentDirection = 0;
            FaceWest();
        }
    }

    protected void EastWestActives()
    {
        if (currentDirection == 0 || currentDirection == 2)
        {
            northSouthSprite.SetActive(false);
            eastWestSprite.SetActive(true);
        }
        weapon.SetFirePoint(eastWestFirePoint);
    }

    protected void NorthSouthActives()
    {
        if (currentDirection == 1 || currentDirection == 3)
        {
            eastWestSprite.SetActive(false);
            northSouthSprite.SetActive(true);
        }
        weapon.SetFirePoint(northSouthFirePoint);
    }

    public override void FaceNorth()
    {
        NorthSouthActives();
        
        currentDirection = 0;
    }

    public override void FaceEast()
    {
        EastWestActives();
        eastWestSprite.GetComponent<SpriteRenderer>().flipY = true ;
        currentDirection = 1;
    }

    public override void FaceSouth()
    {
        NorthSouthActives();
        
        currentDirection = 2;
    }

    public override void FaceWest()
    {
        EastWestActives();
        eastWestSprite.GetComponent<SpriteRenderer>().flipY = false;
        currentDirection = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
