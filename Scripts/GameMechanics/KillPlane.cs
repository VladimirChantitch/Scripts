using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour{
    void OnTriggerEnter(Collider other){
        if (other.TryGetComponent<PlayerManager>(out PlayerManager playerManager)){
            GameManager.Instance.HandlePlayerDeath();
        }
    }
}
