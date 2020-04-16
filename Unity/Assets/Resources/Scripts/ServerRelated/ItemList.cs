using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemList
{

    ////////////////////
    // Singleton Shit //
    ////////////////////

    private static readonly object padlock = new object();
    private static ItemList instance;
    private GameObject soul;
    private int maxSouls = 6;

    private List<GameObject> itemsNoSoul;

    private ItemList() //NOTE: because I wrote this fast, when AllItems is gotten, soul will be id 0. meaning that these item ids are offset by one
    {
        itemsNoSoul = new List<GameObject>
        {
            Resources.Load("Prefabs/PickupPrefab/Items/GrenadePickup") as GameObject,
            Resources.Load("Prefabs/PickupPrefab/Items/JokeGrenadePickup") as GameObject
           
        };

        soul = Resources.Load("Prefabs/PickupPrefab/SoulPickup") as GameObject;
    }

    public static ItemList Instance {
        get {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ItemList();
                }
                return instance;
            }
        }
    }


    public List<GameObject> AllItems {
        get {
            List<GameObject> res = new List<GameObject>();
            res.Add(soul);
            res.AddRange(itemsNoSoul);
            return res;
        }
    }

    public GameObject getRandomWithSouls(int souls)
    {
        Debug.Log("getting loot");
        List<GameObject> options;
        if (souls <= maxSouls)
        {
            options = AllItems;
        }
        else
        {
            options = itemsNoSoul;
        }
        return options[Random.Range(0, options.Count)];
    }
}
