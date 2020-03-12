using UnityEngine;
using System.Collections;

public class StartingRoom : Room
{
    public GameObject player;
    public GameObject startingRoomMap;
    public GameObject mainCamera;

    void Start()
    {
        JobId = -1;
        RoomLayout = startingRoomMap;

        GameObject instatiatedPlayer = Instantiate(player, roomLayout.SpawnLocation);
        instatiatedPlayer.GetComponent<Player>().mainCam = mainCamera.GetComponent<Camera>();
        mainCamera.GetComponent<CameraController>().player = instatiatedPlayer.transform;

        enterRoom();
    }

}
