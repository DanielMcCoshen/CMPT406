using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathTrapEnterPassiveTutorialEnemy : OnDeathTrapEnterEnemyBasic
{
    public TutorialCombatRoomMap roomMap;

    public override void OnDeathTrapTrigger(string trapType)
    {
        roomMap.KilledPassiveEnemy();
        Destroy(gameObject.transform.parent.gameObject);
    }
}
