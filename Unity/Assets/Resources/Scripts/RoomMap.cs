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

    public int filterID;

    public Room room;

    public Transform SpawnLocation { get => spawnLocation; }

    [Header("Tilemap References")]
    public GameObject bridges;
    public GameObject dynamicDanger;

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
        return enemies.Count == 0;
    }

    public void switchDynamicDanger(bool option)
    {
        dynamicDanger.SetActive(option);
        bridges.SetActive(!option);
    }

    public void activateRoom(GameObject playerDeathTrigger)
    {
        playerDeathTrigger.GetComponent<OnDeathTrapEnterPlayer>().SetRespawnPosition(spawnLocation);
        room.enterRoom();
    }
}
