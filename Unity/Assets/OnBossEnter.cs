using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBossEnter : MonoBehaviour
{

    OnDeathTrapEnterPlayer playerDeathCollider;
    musicJukebox jukebox;
    string directoryToPlayerDeathCollider = "/PlayerCameraAndCanvas/PlayerContainer/DeathCollider";
    string directoryToJukebox = "/PlayerCameraAndCanvas/PlayerContainer/Jukebox";
    public AudioSource entrySFX;

    // Start is called before the first frame update
    void Start()
    {
        entrySFX.Play();
        playerDeathCollider = GameObject.Find(directoryToPlayerDeathCollider).GetComponent<OnDeathTrapEnterPlayer>();
        playerDeathCollider.respawnPosition = this.transform.position;
        jukebox = GameObject.Find(directoryToJukebox).GetComponent<musicJukebox>();
        jukebox.PlayCombatTheme();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
