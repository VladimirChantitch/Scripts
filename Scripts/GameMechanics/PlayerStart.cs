using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour{
    public static PlayerStart Instance {get; private set;}
    private void Awake(){
        Instance = this;
    }
    [SerializeField] private GameObject playerCharacter_GO;
    private void Start(){
        Instantiate(playerCharacter_GO, transform.position, transform.rotation);
    }
    public void RespawnPlayerAfterDeath(){
        UI_Manager.Instance.UpdateUI();
        Instantiate(playerCharacter_GO, transform.position, transform.rotation);
    }
}
