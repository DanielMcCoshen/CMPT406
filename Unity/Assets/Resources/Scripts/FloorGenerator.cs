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
    public List<List<Room>> rooms;

    void Start()
    {
        layout = new FloorLayout(floorSize, new Point(startRoomX, startRoomY), new RangeInt(minRooms,maxRooms));
        using (StreamWriter writetext = File.CreateText("layout.txt"))
        {
            writetext.WriteLine(layout);
        }
        layout.Apply(rooms);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
