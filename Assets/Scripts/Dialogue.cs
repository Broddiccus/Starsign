using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Transform camMoveLoc;
    public Sprite[] portraits;
    public string[] names;
    [TextArea(3, 10)]
    public string[] sentences;
    public int[] eventloc;
    public string[] eventname;
}

//this is called from DialogueTrigger, which has a triggerbox attatched to it to call the GameManager and print all the dialouge stored in this class to the dialogue boxes it's holding
//if you want to circumvent the triggerbox just call TriggerDialogue() in DialogueTrigger seperately
//the main thing here is is eventloc tells the GameManager WHEN to call a function with specific information about an event
//the eventname holds WHAT function to call, so if you want to do an event
//make eventloc the number of the dialogue you want to have the stuff happen on
//make eventname the name of a unique event that you will then write in EVENTCOMPENDIUM which is called from the GameManager, which the EVENTCOMPENDIUM can reference freely
