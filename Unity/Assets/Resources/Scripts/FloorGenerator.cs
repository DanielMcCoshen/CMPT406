using UnityEngine;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

public class FloorGenerator : MonoBehaviour
{

    private FloorLayout layout;

    public int minRooms;
    public int maxRooms;
    public int startRoomX;
    public int startRoomY;
    public RectInt floorSize;
    public List<ListWrapper> rooms;
    public int numberOfRooms;
    public int totalNumberOfRooms;
    private bool bossRoomSpawned = false;

    public StartingRoom startRoom;

    void Start()
    {
        layout = new FloorLayout(floorSize, new Point(startRoomX, startRoomY), new RangeInt(minRooms,maxRooms));
        using (StreamWriter writetext = File.CreateText("layout.txt"))
        {
            writetext.WriteLine(layout);
        }
        numberOfRooms = layout.NumberOfRooms - 1;
        totalNumberOfRooms = layout.NumberOfRooms;
        Debug.Log("floor generator rooms" + numberOfRooms);
        layout.Apply(constructLists(rooms));
        updateRoomsFloorGeneratorField(rooms);
        
        startRoom.StartGame();
    }

    public void ReduceNumberOfRooms()
    {
        numberOfRooms -= 1;

    }

    private void updateRoomsFloorGeneratorField(List<ListWrapper> rooms)
    {
        foreach (ListWrapper l in rooms)
        {
            foreach (Room r in l.list)
            {
                r.SetFloorGenerator(gameObject.GetComponent<FloorGenerator>());
            }
        }
        
    }

    private List<List<Room>> constructLists(List<ListWrapper> whyDoYouMakeMeDoThisUnity)
    {
        List<List<Room>> res = new List<List<Room>>();
        foreach(ListWrapper l in whyDoYouMakeMeDoThisUnity)
        {
            res.Add(l.list);
        }
        res.Reverse();
        return res;
    }

    [System.Serializable]
    public class ListWrapper
    {
        public List<Room> list;
    }

    public bool ReadyForBossRoom()
    {
        if (!bossRoomSpawned)
        {
            float roomsTravelledPercent = numberOfRooms / totalNumberOfRooms;
            if (numberOfRooms <= 1)
            {
                bossRoomSpawned = true;
                return true;
            }
            else if (roomsTravelledPercent <= .666666667 && Random.Range(0.0f, 1.0f) >= roomsTravelledPercent + .30)
            {
                bossRoomSpawned = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
