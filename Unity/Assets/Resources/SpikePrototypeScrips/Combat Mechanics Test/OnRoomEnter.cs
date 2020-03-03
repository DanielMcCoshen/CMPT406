using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoomEnter : MonoBehaviour
{
    public RoomMap roomMap;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "DeathPit")
        {
            //roomMap.activateRoom();
        }
    }
}
