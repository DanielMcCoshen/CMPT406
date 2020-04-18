using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgusRespawn : MonoBehaviour  
{
    //The two spawnpoints for the boss room
    public GameObject spawn1;
    public GameObject spawn2;

    //Starting spawn
    private GameObject currentSpawn;


    // Start is called before the first frame update
    void Start()
    {
        currentSpawn = spawn1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Switch the current spawn point to avoid getting snowballed
    public void SwitchSpawn()
    {
        if(currentSpawn == spawn1)
        {
            currentSpawn = spawn2;
        }
        else
        {
            currentSpawn = spawn1;
        }
    }

    //Get the transform of the current respawn
    public Transform GetSpawn()
    {
        return currentSpawn.transform;
    }
}
