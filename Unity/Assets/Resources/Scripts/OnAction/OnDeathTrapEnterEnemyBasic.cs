using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathTrapEnterEnemyBasic : OnDeathTrapEnter
{
    public AudioSource deathAudio;
    private bool readyToDie = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (readyToDie && !(deathAudio.isPlaying))
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public override void OnDeathTrapTrigger(string trapType)
    {
        deathAudio.Play();
        readyToDie = true;
    }

    public override string NameForDeathTrap()
    {
        return gameObject.transform.parent.gameObject.name;
    }
}
