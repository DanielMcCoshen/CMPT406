using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomMap : MonoBehaviour
{
    [SerializeField]
    private Transform spawnLocation;

    public List<GameObject> enemies = new List<GameObject>();

    [SerializeField]
    private Vector2 size;

    [SerializeField]
    private GameObject loot = null;

    public Vector2 Size { get => size; }

    public Room room;

    public Transform SpawnLocation { get => spawnLocation; }

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

    public void setDynamicDanger(bool option)
    {
        dynamicDanger.SetActive(option);
    }

    public void activateRoom()
    {
        room.enterRoom();
    }
}
