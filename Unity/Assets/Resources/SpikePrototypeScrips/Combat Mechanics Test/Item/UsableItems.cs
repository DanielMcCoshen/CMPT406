using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItems : MonoBehaviour
{
    public string itemType;
    public int numberOfUses;
    public Shooting playerShooting;

    public Transform firePoint;
    public Transform aimPoint;
    public GameObject ItemMenuPrefab;

    public virtual void SetUpItem(Shooting shoot)
    {
        playerShooting = shoot;
        firePoint = playerShooting.GetFirePoint();
        aimPoint = playerShooting.GetAimPoint();
    }

    public virtual void ItemActivation()
    {

    }

    public virtual void UseItem()
    {
        ItemActivation();

        numberOfUses-=1;
        if(numberOfUses <= 0)
        {
            playerShooting.ItemUsedUp(itemType);
        }
    }
}
