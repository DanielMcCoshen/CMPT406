using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject itemPrefab;

    public GameObject GetItem()
    {
        return itemPrefab;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
