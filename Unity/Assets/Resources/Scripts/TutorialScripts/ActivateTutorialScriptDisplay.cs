using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTutorialScriptDisplay : MonoBehaviour
{
    public Dictionary<string, string[]> messageSets = new Dictionary<string, string[]>();
    public TutorialTypeOutInstructions typer;

    void Start()
    {
        messageSets.Add("Shooting", new string[]{"The bones of the damned, cursed remnants of lost souls.",
            "Aim thine spear, wanderer, and use the power of MOUSE LEFT CLICK to knock it into the abyss."});
        messageSets.Add("Items", new string[] { "Well done, wanderer",
            "I have created a bomb for thee over yonder, move to pick it up." });
        messageSets.Add("Death", new string[] {"When thy soul is thrown into the abyss I can reconsitute it",
            "The number of times I can do so remotely is equal to the soulstones you have acquired.",
            "Try to avoid floating too quickly or being pushed by thine enemies' attacks.",
            "Now, with the power of RIGHT MOUSE CLICK, aim light and toss thine bomb to dispel thy foes"});
        messageSets.Add("Final", new string[] {"Well done, now you are prepared to traverse the mists and earn passage.",
        "Go to the next room and enter the portal, these allow you to travel to other areas along the islands of the Styx.",
        "Beware, portals will lead to dangerous creatures lairs but also to thine gold coins."});
    }

    public void ActivateMessages(string messageSet, bool playerMayWalkAfterActivation, 
        bool playerMayShootAfterActivation, bool playerMayUseItemsAfterActivation)
    {
        typer.WriteOutMessages(messageSets[messageSet], playerMayWalkAfterActivation,
                playerMayShootAfterActivation, playerMayUseItemsAfterActivation);
    }
}
