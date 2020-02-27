using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomCodeTextManager : MonoBehaviour
{
   
    public TextMeshProUGUI gameIDText;
    public TextMeshProUGUI RoomPlayerList;

    public void updateRoomId()
    {
        gameIDText.SetText("Room Code: " + ServerInfo.Instance.RoomCode);
    }
}
