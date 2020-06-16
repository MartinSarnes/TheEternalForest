﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class DialogueManager1 : MonoBehaviour
{
    public GameObject dBox;
    public Text dText;
    public bool dialogActive;
    public string[] dialogLines;
    public int currentLine;

    void Start()
    {
        
    }

    void Update()
    {
        if (dialogActive && CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            Debug.Log("I am pressed");
            //dBox.SetActive(false);
            //dialogActive = false;
            currentLine++;
        }
        if (currentLine >= dialogLines.Length)
        {
            dBox.SetActive(false);
            dialogActive = false;

            currentLine = 0;
        }
        dText.text = dialogLines[currentLine];
    }
    public void ShowBox(string dialogue)
    {
        dialogActive = true;
        dBox.SetActive(true);
        dText.text = dialogue;
    }
    public void ShowDialogue()
    {
        dBox.SetActive(true);
        dialogActive = true;
    }
}
