using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    public string gameScene;
    public TextMeshProUGUI rosterText;
    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void StartMenuUpdate()
    {
        StartCoroutine(UpdateRosterLoop());
    }

    private IEnumerator UpdateRosterLoop()
    {
        while (true)
        {
            string roster = "";
            yield return new WaitForSeconds(1f);
            foreach (PlayerInfo player in ServerInfo.Instance.Players)
            {
                    roster += player.name + '\n';
            }
           
            rosterText.SetText(roster);
        }
    }
}
