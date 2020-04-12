using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathTrapEnterEnemyBasic : OnDeathTrapEnter
{
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
        Destroy(gameObject.transform.parent.gameObject);
    }

    public override string NameForDeathTrap()
    {
        return gameObject.transform.parent.gameObject.name;
    }
}
