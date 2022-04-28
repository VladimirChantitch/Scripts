using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour{
    public static GameManager Instance {get; private set;}
    async void Awake(){
        if (Instance == null) Instance = this;

        GameManager[] managers = FindObjectsOfType<GameManager>();
        if (managers.Length > 1){
            Instance = managers[0];
            managers[0].MANDATORY_GO.GetComponentInChildren<UI_Manager>().ForceInstance();
            Destroy(managers[1].MANDATORY_GO);
        }
    }
    [SerializeField] private GameObject MANDATORY_GO;
    [SerializeField] private int deathCount;
    public int GetDeathCount(){
        return deathCount;
    }

    [SerializeField] private string mainMenu;
    [SerializeField] private List<string> Levels = new List<string>();
    [SerializeField] private int currentScene_index;
    public int GetCurrentScene_Index(){
        return currentScene_index;
    }

    [Header("current game OPTIONS")]
    [SerializeField] private float current_sens;
    public void SetSens(float newSens){
        current_sens = newSens;
    }
    public float GetSens(){
        return current_sens;
    }

    void Start(){
        UI_State();
        DontDestroyOnLoad(MANDATORY_GO);
    }
    public void UI_State(){
        if (currentScene_index == -1){
            UI_Manager.Instance.UpdateState(true);
        }else{
            UI_Manager.Instance.UpdateState(false);
        }
    }
    public void UpdateSettings(){
        FindObjectOfType<PlayerManager>().SetSensivity(current_sens);
    }
    public void LoadNextScene(){
        UI_State();
        if (Levels.Count > currentScene_index){
            SceneManager.LoadScene(Levels[currentScene_index+1]);
            UpdateSettings();
            currentScene_index +=1;
            return;
        }else{
            print("you already in the last scene registered");
        }
    }
    public void LoadAScene(int index){
        currentScene_index = index;
        UI_State();
        SceneManager.LoadScene(Levels[index]);
        UpdateSettings();
    }
    public void LoadMainMenuScene(){
        currentScene_index = -1;
        UI_State();
        SceneManager.LoadScene(mainMenu);
    }


    public void HandlePlayerDeath(){
        deathCount += 1;
        UI_Manager.Instance.OpenDeathScreen();
    }
    public void RestartLevel(){
            PlayerManager.Instance.HandleDeath();
            Destroy(PlayerManager.Instance.gameObject);
            PlayerStart.Instance.RespawnPlayerAfterDeath();
            UI_Manager.Instance.CloseDeathScreen();
    }
}
