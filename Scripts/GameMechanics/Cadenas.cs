using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadenas : MonoBehaviour{
    [Header("audio")]
    [SerializeField] AudioSource audioSource;
    public AudioSource GetAudioSource(){
        return audioSource;
    }
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip fail;
    [Header("door stats")]
    [SerializeField] DoorScript doorItOpens;
    [SerializeField] private List<int> digits = new List<int>();
    public List<int> GetDigits(){
        return digits;
    }
    public enum e_digitsColor{Red, Blue, Yellow, Purple, Green};
    [SerializeField] private List<e_digitsColor> digitsColors = new List<e_digitsColor>();
    public List<e_digitsColor> GetDigitsColor(){
        return digitsColors;
    }

    public void CheckIfItsGood(List<int> results){
        for (int i = 0; i < digits.Count; i++){
            if(digits[i] == results[i]){
                continue;
            }else{
                Failled();
                return;
            }
        }
        Openned();
    }

    public void Openned(){
        print("success");
        audioSource.PlayOneShot(success);
        doorItOpens.SetCanOpen(true);
        UI_Manager.Instance.CloseCadenasMenu();
    }

    public void Failled(){
        UI_Manager.Instance.CloseCadenasMenu();
        audioSource.PlayOneShot(fail);
        doorItOpens.SetCanOpen(false);
        print("fail");
    }

}
