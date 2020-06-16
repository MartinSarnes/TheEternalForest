using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class dialogueHolder1 : MonoBehaviour
{
    public string dialogue;
    public string[] dialogueLines;
    private DialogueManager1 dMan;

    void Start()
    {
        dMan = FindObjectOfType<DialogueManager1>();    
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Character")
        {
            //if (CrossPlatformInputManager.GetButtonDown("Fire2"))
            //{
            //}
            //dMan.ShowBox(dialogue);
            if (!dMan.dialogActive)
            {
                dMan.dialogLines = dialogueLines;
                dMan.currentLine = 0;
                dMan.ShowDialogue();
            }
        }    
    }
}
