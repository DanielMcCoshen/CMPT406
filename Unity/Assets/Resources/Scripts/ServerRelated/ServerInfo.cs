using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class ServerInfo
{
    ////////////////////
    // Singleton Shit //
    ////////////////////

    private static readonly object padlock = new object();
    private static ServerInfo instance;
    private ServerInfo()
    {

    }

    public static ServerInfo Instance
    {
        get {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ServerInfo();
                }
                return instance;
            }
        }
    }

    ///////////////////
    // Instance Shit //
    ///////////////////
    
    private string hostname = "";
    private bool hasHostname = false;

    private string roomCode = "";
    private bool hasRoomCode = false;

    private string token = "";
    private bool hasToken = false;

    private List<PlayerInfo> players = new List<PlayerInfo>();

    public string Hostname {
        get {
            if (!hasHostname)
            {
                throw new System.InvalidOperationException("Value not yet set");
            }
            return hostname;
        }
        set{
            if (hasHostname)
            {
                throw new System.InvalidOperationException("Value already set");
            }
            hostname = value;
            hasHostname = true;
        }
    }
    public string RoomCode {
        get {
            if (!hasRoomCode)
            {
                throw new System.InvalidOperationException("Value not yet set");
            }
            return roomCode;
        }
        set {
            if (hasRoomCode)
            {
                throw new System.InvalidOperationException("Value already set");
            }
            roomCode = value;
            hasRoomCode = true;
        }
    }
    public string Token {
        get {
            if (!hasToken)
            {
                throw new System.InvalidOperationException("Value not yet set");
            }
            return token;
        }
        set { 
            if (hasToken)
            {
                throw new System.InvalidOperationException("Value already set");
            }
            token = value;
            hasToken = true;
        }
    }

    public List<PlayerInfo> Players { get => players; set => players = value; }
}
