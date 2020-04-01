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

    public StartingRoom startRoom;

    void Start()
    {
        layout = new FloorLayout(floorSize, new Point(startRoomX, startRoomY), new RangeInt(minRooms,maxRooms));
        using (StreamWriter writetext = File.CreateText("layout.txt"))
        {
            writetext.WriteLine(layout);
        }
        layout.Apply(constructLists(rooms));
        startRoom.StartGame();
    }
    private List<List<Room>> constructLists(List<ListWrapper> whyDoYouMakeMeDoThisUnity)
    {
        List<List<Room>> res = new List<List<Room>>();
        foreach(ListWrapper l in whyDoYouMakeMeDoThisUnity)
        {
            res.Add(l.list);
        }
        return res;
    }

    [System.Serializable]
    public class ListWrapper
    {
        public List<Room> list;
    }
}
