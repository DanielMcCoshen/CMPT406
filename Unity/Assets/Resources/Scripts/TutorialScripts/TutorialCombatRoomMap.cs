using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCombatRoomMap : MonoBehaviour
{
    [SerializeField]
    private Transform spawnLocation = null;
    private bool entered = false;
    public bool spawnedSecondSet = false;
    public bool passiveSkeletonDead = false;
    private bool enemiesAreUnspawned = true;
    private bool grenadeSpawned = false;
    public GameObject itemPickupPrefab;

    public TutorialShooting playerInventory;

    public int filterID;

    public string[] messageSetNames;
    public bool[] messageSetPlayerControlSettings;
    private int messageSetIndex = 0;
    private ActivateTutorialScriptDisplay scriptDisplayActivator = null;

    public List<GameObject> enemies = new List<GameObject>();

    public TutorialCombatRoom room;

    public Transform SpawnLocation { get => spawnLocation; }
    public bool isTempOrGeneric;

    [Header("Tilemap References")]
    public GameObject bridges;
    public GameObject dynamicDanger;
    public GameObject clutter;
    public GameObject instantStopWall;

    void Start()
    {
        scriptDisplayActivator = gameObject.GetComponent<ActivateTutorialScriptDisplay>();
        if (clutter != null && !clutter.Equals(null))
        {
            clutter.SetActive(true);
        }

        if (instantStopWall != null && !instantStopWall.Equals(null))
        {
            instantStopWall.SetActive(false);
            instantStopWall.transform.localScale = new Vector3(1, 0.95f, 1);
        }

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    public void spawnEnemies()
    {
        GameObject enemy = enemies[0];
        if (enemy != null && !enemy.Equals(null))
        {
           enemy.SetActive(true);
        }
    }

    public void KilledPassiveEnemy()
    {
        passiveSkeletonDead = true;
    }

    private void SpawnGrenadePickup()
    {
        grenadeSpawned = true;
        Instantiate(itemPickupPrefab, new Vector3(18f,7.5f,0f), Quaternion.identity);
        ActivateNextTutorialMessageSet();
    }

    void Update()
    {
        if (passiveSkeletonDead && !grenadeSpawned)
        {
            passiveSkeletonDead = false;
            SpawnGrenadePickup();
        }
        else if (grenadeSpawned && playerInventory.HasGrenades() && enemiesAreUnspawned)
        {
            spawnedSecondSet = true;
            enemiesAreUnspawned = false;
            foreach(GameObject enemy in enemies)
            {
                if (enemy != null && !enemy.Equals(null))
                {
                    enemy.SetActive(true);
                }
            }
            ActivateNextTutorialMessageSet();
        }

        if(grenadeSpawned && !playerInventory.HasGrenades() && !enemiesAreUnspawned)
        {
            Instantiate(itemPickupPrefab, spawnLocation.position, Quaternion.identity);
        }

    }

    public void spawnLoot(GameObject loot)
    {
        if (loot != null && !loot.Equals(null))
        {
            Instantiate(loot, spawnLocation);
        }
    }

    public bool isComplete()
    {
        int counter = 0;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && !enemy.Equals(null))
            {
                counter++;
            }
        }
        return 0 == counter;
    }

    public void switchDynamicDanger(bool option)
    {
        dynamicDanger.SetActive(option);
        bridges.SetActive(!option);
        if (instantStopWall != null)
        {
            instantStopWall.SetActive(option);
            instantStopWall.GetComponent<TilemapRenderer>().enabled = !option;
        }
        else
        {
            Debug.LogError(this.gameObject + ": Does not have an instant stop wall.");
        }
    }

    public void ActivateNextTutorialMessageSet()
    {
        scriptDisplayActivator.ActivateMessages(messageSetNames[messageSetIndex],
            messageSetPlayerControlSettings[messageSetIndex*3], 
            messageSetPlayerControlSettings[messageSetIndex * 3 + 1],
            messageSetPlayerControlSettings[messageSetIndex * 3 + 2]);
        messageSetIndex++;
    }

    public void activateRoom(OnDeathTrapEnterPlayer playerDeathTrigger)
    {
        
        if (!entered)
        {
            entered = true;
            playerDeathTrigger.SetRespawnPosition(spawnLocation);
            playerDeathTrigger.MoveToRespawnPosition();
            ActivateNextTutorialMessageSet();
            
        }
        
        room.enterRoom();
    }
}
