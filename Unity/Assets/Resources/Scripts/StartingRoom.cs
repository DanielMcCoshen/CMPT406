using UnityEngine;
using System.Collections;

public class StartingRoom : Room
{
    public GameObject startingRoomMap;

    public GameObject player;


    public void StartGame()
    {
        JobId = -1;
        RoomLayout = startingRoomMap;

        enterRoom();
    }

}
