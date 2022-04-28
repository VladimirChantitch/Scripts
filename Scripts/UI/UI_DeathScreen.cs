using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DeathScreen : MonoBehaviour{
    [SerializeField] private Image rngColor;
    int blue;
    int red;
    int green; 
    public void PlayAgain(){
        UI_Manager.Instance.CloseDeathScreen();
        GameManager.Instance.RestartLevel();
    }

    void OnEnable(){
        StartCoroutine(ColorChange());
    }

    public void GetBackToMainMenu(){
        UI_Manager.Instance.CloseDeathScreen();
        GameManager.Instance.LoadMainMenuScene();
    }

    IEnumerator ColorChange(){
        while(this.isActiveAndEnabled){
            yield return new WaitForSeconds(.35f);
            rngColor.color = new Color(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),1);
        }

        yield return null;
    }
}
