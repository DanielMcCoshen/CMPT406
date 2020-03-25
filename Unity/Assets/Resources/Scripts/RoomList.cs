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

    private List<GameObject> allRooms;

    private RoomList()
    {
        allRooms = new List<GameObject>
        {
            // === All Doors (0-0)===
            Resources.Load("Prefabs/Rooms/30x30/All/Paved Holes") as GameObject,
            // === Corners (1-17) ===
            // N-E (1-4)
            Resources.Load("Prefabs/Rooms/30x30/Corners/N-E/N-E Curve Islands") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/N-E/N-E Large Paneled Ice Room") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/N-E/N-E Skinny Path W_ Branch") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/N-E/N-E W_ Paved Hole") as GameObject,
            // S-E (5-8)
            Resources.Load("Prefabs/Rooms/30x30/Corners/S-E/S-E Curve Islands") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/S-E/S-E Large Paneled Ice Room") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/S-E/S-E Skinny Path W_ Branch") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/S-E/S-E W_ Paved Hole") as GameObject,
            // W-N (9-13)
            Resources.Load("Prefabs/Rooms/30x30/Corners/W-N/W-N Corner Islands") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/W-N/W-N Curve Islands") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/W-N/W-N Large Paneled Ice Room") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/W-N/W-N Skinny Path W_ Branch") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/W-N/W-N W_ Paved Hole") as GameObject,
            // W-S (14-17)
            Resources.Load("Prefabs/Rooms/30x30/Corners/W-S/W-S Curve Islands") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/W-S/W-S Large Paneled Ice Room") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/W-S/W-S Skinny Path W_ Branch") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Corners/W-S/W-S W_ Paved Hole") as GameObject,
            // === Long/Straights (18-26) ===
            // N-S (18-21)
            Resources.Load("Prefabs/Rooms/30x30/Long/N-S/N-S Large Paneled Ice Room") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Long/N-S/N-S Skinny Path W_ Branch") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Long/N-S/N-S Straight Islands") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Long/N-S/N-S W_ Paved Hole") as GameObject,
            // W-E (22-26)
            Resources.Load("Prefabs/Rooms/30x30/Long/W-E/W-E Large Paneled Ice Room") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Long/W-E/W-E Parallel Ice Strips") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Long/W-E/W-E Skinny Path W_ Branch") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Long/W-E/W-E Straight Islands") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Long/W-E/W-E W_ Paved Hole") as GameObject,
            // === Single Doors (27-44)===
            // E (27-31)
            Resources.Load("Prefabs/Rooms/30x30/Single Door/E/E Curve") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/E/E Large Paneled Ice Room") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/E/E Skinny Path W_ Branch") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/E/E Straight Islands") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/E/E W_ Paved Hole") as GameObject,
            // N (32-35)
            Resources.Load("Prefabs/Rooms/30x30/Single Door/N/N Large Paneled Ice Room") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/N/N Skinny Path W_ Branch") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/N/N Straight Islands") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/N/N W_ Paved Hole") as GameObject,
            // S (36-39)
            Resources.Load("Prefabs/Rooms/30x30/Single Door/S/S Large Paneled Ice Room") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/S/S Skinny Path W_ Branch") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/S/S Straight Islands") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/S/S W_ Paved Hole") as GameObject,
            // W (40-44)
            Resources.Load("Prefabs/Rooms/30x30/Single Door/W/W Curve") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/W/W Large Paneled Ice Room") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/W/W Skinny Path W_ Branch") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/W/W Straight Islands") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/Single Door/W/W W_ Paved Hole") as GameObject,
            // === T-Pieces (45-48) ===
            Resources.Load("Prefabs/Rooms/30x30/T Pieces/E-T/E-T Paved Holes") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/T Pieces/N-T/N-T Paved Holes") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/T Pieces/S-T/S-T Paved Holes") as GameObject,
            Resources.Load("Prefabs/Rooms/30x30/T Pieces/W-T/W-T Paved Holes") as GameObject
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
