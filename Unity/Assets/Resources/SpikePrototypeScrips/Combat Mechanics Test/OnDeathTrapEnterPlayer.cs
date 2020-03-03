using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathTrapEnterPlayer : OnDeathTrapEnter
{
    public Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnDeathTrapTrigger(string trapType)
    {
        gameObject.transform.parent.gameObject.transform.position = respawnPosition;
    }

    public override string NameForDeathTrap()
    {
        return gameObject.transform.parent.gameObject.name;
    }
}
