using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour{
    [SerializeField] bool bIsInAMenu;
    public bool GetIsInAMenu(){
        return bIsInAMenu;
    }
    [SerializeField] bool bIsInCadenasMenu; 
    [SerializeField] bool bIsInSettingsMenu;
    [SerializeField] bool bIsInDeathScreen;
    public bool GetIsInSettingsMenu(){
        return bIsInSettingsMenu;
    }
    [Header("references")]
    [SerializeField] private Cadenas LevelCadenas;
    public Cadenas GetLevelCadenas(){
        return LevelCadenas;
    }
    [SerializeField] private UI_Cadenas uI_Cadenas;
    [SerializeField] private UI_SettingsMenu uI_SettingsMenu;
    [SerializeField] private UI_DeathCount uI_DeathCount;
    [SerializeField] private UI_DeathScreen uI_DeathScreen;

    [SerializeField] private GameObject gameOnly_UI;
    [SerializeField] private GameObject mainMenuOnly_UI;

    public static UI_Manager Instance { get; private set;}
    private void Awake(){
        if (Instance == null) Instance = this;
    }
    public void ForceInstance(){
        Instance = this;
    }
    public void UpdateState(bool bIsInMainMenu){
        if (bIsInMainMenu){
            gameOnly_UI.SetActive(false);
            mainMenuOnly_UI.SetActive(true);
        }else{
            mainMenuOnly_UI.SetActive(false);
            gameOnly_UI.SetActive(true);
        }   
    }
    private void OpenAMenu(GameObject menu){
        bIsInAMenu = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        uI_DeathCount.gameObject.SetActive(false);
        menu.SetActive(true);
    }
    private void CloseAMenu(GameObject menu){
        bIsInAMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        uI_DeathCount.gameObject.SetActive(true);
        menu.SetActive(false);
    }

    public void OpenCadenasMenu(Cadenas cadenas){
        LevelCadenas = cadenas;
        bIsInCadenasMenu = true;
        OpenAMenu(uI_Cadenas.gameObject);
        uI_Cadenas.Initialize(cadenas);
    }
    public void CloseCadenasMenu(){
        bIsInCadenasMenu = false;
        CloseAMenu(uI_Cadenas.gameObject);
    }

    public void OpenSettingsMenu(){
        bIsInSettingsMenu = true;
        OpenAMenu(uI_SettingsMenu.gameObject);
    }
    public void CloseSettingMenu(){
        bIsInSettingsMenu = false;
        CloseAMenu(uI_SettingsMenu.gameObject);
    }
    
    public void OpenDeathScreen(){
        bIsInDeathScreen = true;
        OpenAMenu(uI_DeathScreen.gameObject);
    }

    public void CloseDeathScreen(){
        bIsInDeathScreen = false;
        CloseAMenu(uI_DeathScreen.gameObject);
    }

    public void UpdateUI(){
        uI_DeathCount.UpdateDeathCount();
    }
}
