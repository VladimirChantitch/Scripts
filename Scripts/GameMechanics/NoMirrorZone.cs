using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMirrorZone : MonoBehaviour{
    [SerializeField] private AudioClip nopeClip;
    [SerializeField] private AudioSource audioSource;
    void OnTriggerEnter(Collider other){
        if(other.TryGetComponent<Mirror>(out Mirror mirror)){
            audioSource.Play();
            Destroy(mirror.gameObject);
        }
    }
}
