using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCombatRoom : MonoBehaviour
{

    protected GameObject roomLayoutObj;
    private bool reducedNumberOfRooms = false;

    private bool enemiesDefeated = false;
    private bool locked = false;

    public TutorialCombatRoomMap roomLayout;

    void Start()
    {
    }



    public void enterRoom()
    {
        if (!enemiesDefeated)
        {
            roomLayout.switchDynamicDanger(true);
            roomLayout.spawnEnemies();
            locked = true;
        }
    }

    public void FixedUpdate()
    {
        if (locked)
        {
            if (roomLayout.isComplete())
            {
                enemiesDefeated = true;
                locked = false;
                roomLayout.switchDynamicDanger(false);
            }
        }
    }
}
