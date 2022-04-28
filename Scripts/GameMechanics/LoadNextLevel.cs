using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour{
    [SerializeField] private string levelName;
    void OnTriggerEnter(Collider other){
        GameManager.Instance.LoadNextScene();
    }
}
