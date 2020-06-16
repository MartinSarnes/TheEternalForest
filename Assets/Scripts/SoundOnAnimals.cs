using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SoundOnAnimals : MonoBehaviour
{
    public AudioSource animalSound;
    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animalSound.enabled = true;
        }
        
    }
   
    
    
}
