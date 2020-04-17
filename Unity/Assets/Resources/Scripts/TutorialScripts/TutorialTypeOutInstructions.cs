using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTypeOutInstructions : MonoBehaviour
{
    public GameObject text;
    public Text writeTo;
    bool locked = false;
    bool writingOut = false;
    bool finishTyping = false;
    public GameObject button;
    public string[] initialMessages;
    private string[] currentMessages= new string[0];
    private int currentIndex = 0;
    public TutorialPlayerMovement playerMovement;
    public TutorialShooting shooting;
    protected bool playerMayMove= false;
    protected bool playerMayShoot = false;
    protected bool playerMayUseItems = false;
    public Rigidbody2D player;

    void Start()
    {
        text.SetActive(false);
        WriteOutMessages(initialMessages, true,true,true);
    }
    
    void Update()
    {
        if (locked && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (writingOut)
            {
                finishTyping = true;
            }
            else
            {
                WriteNext();
            }
        }
        else if (currentIndex == currentMessages.Length-1 
            && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            StopTyping();
            currentIndex = -1;
        }
    }

    public void WriteOutMessages(string[] messages, bool plyrMayMove, bool plyrMayShoot, bool plyrMayUseItems)
    {
        player.velocity = new Vector3(0, 0, 0);
        playerMayMove = plyrMayMove;
        playerMayShoot = plyrMayShoot;
        playerMayUseItems = plyrMayUseItems;
        button.SetActive(false);
        playerMovement.SetPlayersAbilityToMove(false);
        shooting.SetIfPlayerCanShoot(false);
        shooting.SetIfPlayerCanUseItems(false);
        locked = true;
        text.SetActive(true);
        currentMessages = messages;
        currentIndex = -1;
        WriteNext();
    }

    public void StopTyping()
    {
        playerMovement.SetPlayersAbilityToMove(playerMayMove);
        shooting.SetIfPlayerCanShoot(playerMayShoot);
        shooting.SetIfPlayerCanUseItems(playerMayUseItems);
        button.SetActive(false);
        text.SetActive(false);
    }

    private void WriteNext()
    {
        button.SetActive(false);
        if (currentIndex < currentMessages.Length)
        {
            currentIndex++;
            writingOut = true;
            finishTyping = false;
            StartCoroutine(TypeIt());
        }
        else
        {
            locked = false;
        }
        
    }

    IEnumerator TypeIt()
    {
        writeTo.text = "";
        foreach (char letter in currentMessages[currentIndex].ToCharArray())
        {
            if (finishTyping)
            {
                writeTo.text = currentMessages[currentIndex];
            }
            else
            {
                writeTo.text += letter;
                yield return new WaitForSecondsRealtime(0.03f);
            }
        }
        writingOut = false;
        finishTyping = false;
        button.SetActive(true);
        if (currentIndex == currentMessages.Length - 1)
        {
            locked = false;
        }
    }
}
