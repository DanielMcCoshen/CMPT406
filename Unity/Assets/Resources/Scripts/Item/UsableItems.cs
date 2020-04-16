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

    public GameObject GetItemMenuPrefab()
    {
        return ItemMenuPrefab;
    }

    public string GetItemType()
    {
        return itemType;
    }

    public virtual void SetUpItem(GameObject player)
    {
        playerShooting = player.GetComponent<Shooting>();
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

    public int GetNumberOfUses()
    {
        return numberOfUses;
    }

    public void AddItemUsages(int numOfUsesToAdd)
    {
        if (numOfUsesToAdd > 0)
        {
            numberOfUses += numOfUsesToAdd;
        }
        
    }
}
