using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_CadenasCell : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int currentDigit;
    [SerializeField] private int currentIndex;
    public int GetDigit(){
        return currentDigit;
    }

    public void Initialize(int index){
        currentIndex = index;
        currentDigit = 0;
        text.text = currentDigit.ToString();
    }

    public void Plus(){
        currentDigit += 1;
        if (currentDigit >= 10){
            currentDigit = 9;
        }
        UI_Cadenas.Instance.SetResults(currentIndex, currentDigit);
        text.text = currentDigit.ToString();
        UI_Manager.Instance.GetLevelCadenas().GetAudioSource().PlayOneShot(buttonClip);
    }

    public void Minus(){
        currentDigit -= 1;
        if (currentDigit < 0){
            currentDigit = 0;
        }
        text.text = currentDigit.ToString();
        UI_Manager.Instance.GetLevelCadenas().GetAudioSource().PlayOneShot(buttonClip);
    }
}
