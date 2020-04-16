using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class RoomMap : MonoBehaviour
{
    [SerializeField]
    private Transform spawnLocation = null;

    public int filterID;

    public List<GameObject> enemies = new List<GameObject>();

    public Room room;

    public Transform SpawnLocation { get => spawnLocation; }
    public bool isTempOrGeneric;

    [Header("Tilemap References")]
    public GameObject bridges;
    public GameObject dynamicDanger;
    public GameObject clutter;
    public GameObject instantStopWall;

    void Start()
    {
        if (clutter != null)
        {
            clutter.SetActive(true);
        }
        
        if (instantStopWall != null) {
            instantStopWall.SetActive(false);
            instantStopWall.transform.localScale = new Vector3(1, 0.95f, 1);
        } else {
            if (!isTempOrGeneric) {
                Debug.LogError(this.gameObject + ": Does not have an instant stop wall.");
            }
        }
        
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    public void spawnEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null) {
                enemy.SetActive(true);
            }
        }
    }

    public void spawnLoot(GameObject loot)
    {
        if (loot != null)
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
        if (instantStopWall != null) {
            instantStopWall.SetActive(option);
            instantStopWall.GetComponent<TilemapRenderer>().enabled = !option;
        } else {
            Debug.LogError(this.gameObject + ": Does not have an instant stop wall.");
        }
    }

    public void activateRoom(OnDeathTrapEnterPlayer playerDeathTrigger)
    {

        playerDeathTrigger.SetRespawnPosition(spawnLocation);
        room.enterRoom();
    }
}
