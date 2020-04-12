﻿using System.Collections;
using UnityEngine;

public class GameRoomManager : MonoBehaviour
{
    public string ServerHostName = "localhost:5000";
    public  RoomCodeTextManager textManager;
    public MainMenu mainMenu;
    public float pollTimer;
    async void Start()
    {
        DontDestroyOnLoad(gameObject);
        mainMenu.StartMenuUpdate();
        try
        {
            ServerInfo.Instance.Hostname = "http://" + ServerHostName;
            await NetworkManager.CreateGame();
        }
        catch (System.InvalidOperationException) { } //this squelches errors that happen when returning to the main menu. its bad practice but this shit is due in a week
        textManager.updateRoomId();
        StartCoroutine(UpdateRoomMembersLoop());
        
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

