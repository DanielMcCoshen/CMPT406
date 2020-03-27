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

[System.Serializable]
public class PostJobResponse
{
    public int job_id;
    public string error;
}

public class CheckJobResponse
{
    public bool complete;
    public int result;
}

[System.Serializable]
public class VoteOptions
{
    public int filter_id;
    public int type;
    public int level;
}