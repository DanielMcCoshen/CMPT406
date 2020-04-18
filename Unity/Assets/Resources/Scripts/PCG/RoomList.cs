using System;
using System.Resources;
using System.Runtime.Versioning;
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
    public GameObject BossRoom { get; }
    public GameObject ArgusRoom { get; }

    private List<GameObject> allRooms;

    private String roomsDirectory = "Prefabs/Rooms/Generation Input/";

    private RoomList()
    {
        allRooms = new List<GameObject>
        {
            // === All Doors (0-0)===
            Resources.Load(roomsDirectory + "All/Paved Holes") as GameObject,
            // === Corners (1-17) ===
            // N-E (1-4)
            Resources.Load(roomsDirectory + "Corners/N-E/N-E Curve Islands") as GameObject,
            Resources.Load(roomsDirectory + "Corners/N-E/N-E Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "Corners/N-E/N-E Skinny Path W_ Branch") as GameObject,
            Resources.Load(roomsDirectory + "Corners/N-E/N-E W_ Paved Hole") as GameObject,
            // S-E (5-8)
            Resources.Load(roomsDirectory + "Corners/S-E/S-E Curve Islands") as GameObject,
            Resources.Load(roomsDirectory + "Corners/S-E/S-E Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "Corners/S-E/S-E Skinny Path W_ Branch") as GameObject,
            Resources.Load(roomsDirectory + "Corners/S-E/S-E W_ Paved Hole") as GameObject,
            // W-N (9-13)
            Resources.Load(roomsDirectory + "Corners/W-N/W-N Corner Islands") as GameObject,
            Resources.Load(roomsDirectory + "Corners/W-N/W-N Curve Islands") as GameObject,
            Resources.Load(roomsDirectory + "Corners/W-N/W-N Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "Corners/W-N/W-N Skinny Path W_ Branch") as GameObject,
            Resources.Load(roomsDirectory + "Corners/W-N/W-N W_ Paved Hole") as GameObject,
            // W-S (14-17)
            Resources.Load(roomsDirectory + "Corners/W-S/W-S Curve Islands") as GameObject,
            Resources.Load(roomsDirectory + "Corners/W-S/W-S Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "Corners/W-S/W-S Skinny Path W_ Branch") as GameObject,
            Resources.Load(roomsDirectory + "Corners/W-S/W-S W_ Paved Hole") as GameObject,
            // === Long/Straights (18-26) ===
            // N-S (18-21)
            Resources.Load(roomsDirectory + "Long/N-S/N-S Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "Long/N-S/N-S Skinny Path W_ Branch") as GameObject,
            Resources.Load(roomsDirectory + "Long/N-S/N-S Straight Islands") as GameObject,
            Resources.Load(roomsDirectory + "Long/N-S/N-S W_ Paved Hole") as GameObject,
            // W-E (22-26)
            Resources.Load(roomsDirectory + "Long/W-E/W-E Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "Long/W-E/W-E Parallel Ice Strips") as GameObject,
            Resources.Load(roomsDirectory + "Long/W-E/W-E Skinny Path W_ Branch") as GameObject,
            Resources.Load(roomsDirectory + "Long/W-E/W-E Straight Islands") as GameObject,
            Resources.Load(roomsDirectory + "Long/W-E/W-E W_ Paved Hole") as GameObject,
            // === Single Doors (27-44)===
            // E (27-31)
            Resources.Load(roomsDirectory + "Single Door/E/E Curve") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/E/E Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/E/E Skinny Path W_ Branch") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/E/E Straight Islands") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/E/E W_ Paved Hole") as GameObject,
            // N (32-35)
            Resources.Load(roomsDirectory + "Single Door/N/N Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/N/N Skinny Path W_ Branch") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/N/N Straight Islands") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/N/N W_ Paved Hole") as GameObject,
            // S (36-39)
            Resources.Load(roomsDirectory + "Single Door/S/S Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/S/S Skinny Path W_ Branch") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/S/S Straight Islands") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/S/S W_ Paved Hole") as GameObject,
            // W (40-44)
            Resources.Load(roomsDirectory + "Single Door/W/W Curve") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/W/W Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/W/W Skinny Path W_ Branch") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/W/W Straight Islands") as GameObject,
            Resources.Load(roomsDirectory + "Single Door/W/W W_ Paved Hole") as GameObject,
            // === T-Pieces (45-48) ===
            Resources.Load(roomsDirectory + "T Pieces/E-T/E-T Paved Holes") as GameObject,
            Resources.Load(roomsDirectory + "T Pieces/N-T/N-T Paved Holes") as GameObject,
            Resources.Load(roomsDirectory + "T Pieces/S-T/S-T Paved Holes") as GameObject,
            Resources.Load(roomsDirectory + "T Pieces/W-T/W-T Paved Holes") as GameObject,
            // === Room Expansion A (49-53) ===
            Resources.Load(roomsDirectory + "All/Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "T Pieces/E-T/E-T Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "T Pieces/N-T/N-T Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "T Pieces/S-T/S-T Large Paneled Ice Room") as GameObject,
            Resources.Load(roomsDirectory + "T Pieces/W-T/W-T Large Paneled Ice Room") as GameObject,
            // === Room Expansion A1 (54)
            Resources.Load(roomsDirectory + "Long/N-S/N-S Parallel Ice Strips") as GameObject
        };
        BossRoom =  Resources.Load(roomsDirectory + "BossPortal") as GameObject;
        ArgusRoom = Resources.Load(roomsDirectory + "ArgusPortal") as GameObject;
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

    public GameObject getRandomWithFilter(Filter f)
    {
        List<GameObject> options = new List<GameObject>();
        foreach (GameObject room in allRooms)
        {
            if (room.GetComponent<RoomMap>().filterID == f.Id)
            {
                options.Add(room);
            }
        }
        return options[UnityEngine.Random.Range(0, options.Count)];
    }
}
