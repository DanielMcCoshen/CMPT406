using UnityEngine;
using System.Collections;

public class StartingRoom : Room
{
    public GameObject startingRoomMap;

    public GameObject player;

    void Start()
    {
        player.SetActive(false);
    }

    public void StartGame()
    {
        player.SetActive(true);
        JobId = -1;
        RoomLayout = startingRoomMap;

        enterRoom();
    }

}
