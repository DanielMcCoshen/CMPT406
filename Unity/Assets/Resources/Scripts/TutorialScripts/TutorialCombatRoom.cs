using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCombatRoom : MonoBehaviour
{

    protected GameObject roomLayoutObj;
    private bool reducedNumberOfRooms = false;

    private bool enemiesDefeated = false;

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
        }
    }

    public void FixedUpdate()
    {
    }
}
