using System.Collections.Generic;

[System.Serializable]
public class RoomConfig
{
    public string room_id;
    public string token;
}

[System.Serializable]
public class PlayerInfo
{
    public string name;
    public string colour;
    public int mischeif_points;
}

[System.Serializable]
public class PlayerList
{
    public List<PlayerInfo> users;
}