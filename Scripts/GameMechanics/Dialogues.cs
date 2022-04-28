using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip dialogueClip;
    [SerializeField] private bool Said; 

    void OnTriggerEnter(Collider other){
        if(Said){
            return;
        }
        if (other.TryGetComponent<PlayerManager>(out PlayerManager playerManager)){
            Said = true;
            audioSource.PlayOneShot(dialogueClip);
        }
    }

}
