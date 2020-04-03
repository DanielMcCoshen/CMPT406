using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomMap : MonoBehaviour
{
    [SerializeField]
    private Transform spawnLocation = null;

    public int filterID;

    public List<GameObject> enemies = new List<GameObject>();

    [SerializeField]
    private GameObject loot = null;

    public Room room;

    public Transform SpawnLocation { get => spawnLocation; }

    [Header("Tilemap References")]
    public GameObject bridges;
    public GameObject dynamicDanger;

    void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    public void spawnEnemies()
    {
        Debug.Log("Spawning Enemies");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }

    public void spawnLoot()
    {
        if (loot != null)
        {
            loot.SetActive(true);
        }
    }

   public bool isComplete()
    {
        int counter = 0;
        foreach (GameObject enemy in enemies)
        {
            if(enemy != null && !enemy.Equals(null))
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
    }

    public void activateRoom(OnDeathTrapEnterPlayer playerDeathTrigger)
    {
        
        playerDeathTrigger.SetRespawnPosition(spawnLocation);
        room.enterRoom();
    }
}
