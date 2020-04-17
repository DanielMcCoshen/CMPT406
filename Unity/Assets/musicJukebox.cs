using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicJukebox : MonoBehaviour
{
    [Header("Music")]
    public AudioSource combatTheme;
    public AudioSource explorationTheme;

    // Start is called before the first frame update
    void Start()
    {
        PlayExplorationTheme();
    }

    public void PlayCombatTheme() {
        explorationTheme.Pause();
        combatTheme.Play();
    }

    public void PlayExplorationTheme() {
        combatTheme.Pause();
        explorationTheme.Play();
    }
}
