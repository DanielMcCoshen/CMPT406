using System.Collections;
using UnityEngine;

public class GameRoomManager : MonoBehaviour
{
    public string ServerHostName = "localhost:5000";
    public  RoomCodeTextManager textManager;
    public MainMenu mainMenu;
    public float pollTimer;
    async void Start()
    {
        ServerInfo.Instance.Hostname = "http://" + ServerHostName;
        await NetworkManager.CreateGame();
        textManager.updateRoomId();
        DontDestroyOnLoad(gameObject);
        StartCoroutine(UpdateRoomMembersLoop());
        mainMenu.StartMenuUpdate();
    }

    async void OnApplicationQuit()
    {
        if (NetworkManager.Online)
        {
            await NetworkManager.KillGame();
        }
    }
    private IEnumerator UpdateRoomMembersLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(pollTimer);
            if (NetworkManager.Online)
            {
                _ = NetworkManager.UpdateRoomMembers();
            }
        }
    }
}

