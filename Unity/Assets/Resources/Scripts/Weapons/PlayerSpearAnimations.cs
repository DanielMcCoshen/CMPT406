using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpearAnimations : PlayerWeaponAnimations
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void FaceWest()
    {
        EastWestActives();
        eastWestSprite.GetComponent<SpriteRenderer>().flipX = false;
        currentDirection = 3;
    }

    public override void FaceEast()
    {
        EastWestActives();
        eastWestSprite.GetComponent<SpriteRenderer>().flipX = true;
        currentDirection = 1;
    }
}
