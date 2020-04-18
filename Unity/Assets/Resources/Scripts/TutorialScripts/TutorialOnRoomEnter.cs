using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOnRoomEnter : MonoBehaviour
{
    public TutorialCombatRoomMap roomMap;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null && !col.Equals(null) && col.gameObject.tag == "Player")
        {
            roomMap.activateRoom(col.gameObject.GetComponent<OnDeathTrapEnterPlayer>());
        }
    }
}
