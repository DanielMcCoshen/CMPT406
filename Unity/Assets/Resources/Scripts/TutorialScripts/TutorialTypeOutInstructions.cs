using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTypeOutInstructions : MonoBehaviour
{
    public Text writeTo;
    bool locked = false;
    bool writingOut = false;
    bool finishTyping = false;
    public GameObject button;
    public string[] initialMessages;
    private string[] currentMessages= new string[0];
    private int currentIndex = 0;

    void Start()
    {
        WriteOutMessages(initialMessages);
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
    }

    public void WriteOutMessages(string[] messages)
    {
        locked = true;
        currentMessages = messages;
        currentIndex = -1;
        WriteNext();
    }

    private void WriteNext()
    {
        button.SetActive(false);
        currentIndex++;
        writingOut = true;
        StartCoroutine(TypeIt());
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
    }
}
