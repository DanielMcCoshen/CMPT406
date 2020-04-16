using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBossEnter : MonoBehaviour
{

    OnDeathTrapEnterPlayer playerDeathCollider;
    string directoryToPlayerDeathCollider = "/PlayerCameraAndCanvas/PlayerContainer/DeathCollider";

    // Start is called before the first frame update
    void Start()
    {
        playerDeathCollider = GameObject.Find(directoryToPlayerDeathCollider).GetComponent<OnDeathTrapEnterPlayer>();
        playerDeathCollider.respawnPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
