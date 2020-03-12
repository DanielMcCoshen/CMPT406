﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkManager
{
    private static readonly HttpClient client = new HttpClient();
    public static async Task CreateGame()
    {
        HttpResponseMessage response;
        RoomConfig json;
        try
        {
            response = await client.PostAsync(ServerInfo.Instance.Hostname + "/game", null);
            json = JsonUtility.FromJson<RoomConfig>(await response.Content.ReadAsStringAsync());
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
            return;
        }
        ServerInfo.Instance.RoomCode = json.room_id;
        ServerInfo.Instance.Token = json.token;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ServerInfo.Instance.Token);
    }

    public static async Task KillGame()
    {
        HttpResponseMessage response;
        try
        {
            response = await client.DeleteAsync(ServerInfo.Instance.Hostname + "/game/" + ServerInfo.Instance.RoomCode);
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public static async Task UpdateRoomMembers()
    {
        HttpResponseMessage response;
        PlayerList json;
        try
        {
            response = await client.GetAsync(ServerInfo.Instance.Hostname + "/game/" + ServerInfo.Instance.RoomCode + "/users");
            json = JsonUtility.FromJson<PlayerList>(await response.Content.ReadAsStringAsync());

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return;
        }
        if (json.users != null)
        {
            ServerInfo.Instance.Players = json.users;
        }
        else
        {
            ServerInfo.Instance.Players = new List<PlayerInfo>();
        }
    }

    public static async Task BeginVote(Room room)
    {
        HttpResponseMessage response;
        PostJobResponse json;
        try
        {
            response = await client.PostAsync(ServerInfo.Instance.Hostname + "/game/" + ServerInfo.Instance.RoomCode + "/jobs", null);
            json = JsonUtility.FromJson<PostJobResponse>(await response.Content.ReadAsStringAsync());
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
            return;
        }

        room.JobId = json.job_id;
    }

    public static async Task CheckVote(Room room)
    {
        Debug.Log("Checking Room");
        HttpResponseMessage response;
        checkJobResponse json;
        try
        {
            response = await client.GetAsync(ServerInfo.Instance.Hostname + "/game/" + ServerInfo.Instance.RoomCode + "/jobs/" + room.JobId);
            json = JsonUtility.FromJson<checkJobResponse>(await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return;
        }

        if (json.complete)
        {
            Debug.Log(json.result);
            room.RoomLayout = RoomList.Instance.AllRooms[json.result];
        }
        
    }
}
