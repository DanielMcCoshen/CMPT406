using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomList
{
    ////////////////////
    // Singleton Shit //
    ////////////////////

    private static readonly object padlock = new object();
    private static RoomList instance;

    private List<GameObject> allRooms;

    private RoomList()
    {
        allRooms = new List<GameObject>
        {
            Resources.Load("Prefabs/Rooms/10x20/Bean") as GameObject,
            Resources.Load("Prefabs/Rooms/10x20/Room2") as GameObject
        };
        Debug.Log("RoomList Creation");
    }

    public static RoomList Instance {
        get {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new RoomList();
                }
                return instance;
            }
        }
    }

    
    public List<GameObject> AllRooms { get => allRooms; }

    
}
