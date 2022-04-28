using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SettingsMenu : MonoBehaviour{
    [SerializeField] private Slider sensivitySetting; 
    void OnEnable(){
        sensivitySetting.value = GameManager.Instance.GetSens() / 100;
    }

    public void ValidateData(){
        GameManager.Instance.SetSens(sensivitySetting.value*100);
        UI_Manager.Instance.CloseSettingMenu();
    }

    public void EscapeMenu(){
        UI_Manager.Instance.CloseSettingMenu();
    }

}
