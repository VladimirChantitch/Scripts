using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour{
    private PlayerControls playerControls;
    private bool bJump;
    private bool bSummonMirror;
    private bool bDestroyMirror;
    private bool bInteract;
    private bool bOC_Settings;
    private Vector2 v2_Movement;
    private float mouseX;
    private float mouseY;
    void Awake(){
        playerControls = new PlayerControls();

        playerControls.Action.Summon_Mirror.performed += i => bSummonMirror = true;
        playerControls.Action.Destroy_Mirror.performed += i => bDestroyMirror = true;
        playerControls.Action.Interact.performed += i => bInteract = true;

        playerControls.Movement.Jump.performed += i => bJump = true;

        playerControls.UI.OpenSettings.performed += i => bOC_Settings = true;
    }
    void OnEnable() => playerControls.Enable();
    void OnDisable() => playerControls.Disable();


    public void Update(){
        if (GameManager.Instance.GetCurrentScene_Index() >= 0){ // Game Mode
            mouseX = playerControls.Camera.MouseX.ReadValue<float>();
            mouseY = playerControls.Camera.MouseY.ReadValue<float>();
            PlayerManager.Instance.MoveCamera(mouseX, mouseY);
        
            if (bJump){
                PlayerManager.Instance.TryToJump();
            } 

            if (bSummonMirror){
                PlayerManager.Instance.TryPlacingMirror();
            }

            if (bDestroyMirror){
                PlayerManager.Instance.TryDestroyMirror();
            }

            if (bInteract){
                PlayerManager.Instance.TryInteract();
            }

            if (bOC_Settings){
                if (UI_Manager.Instance.GetIsInSettingsMenu()){
                    UI_Manager.Instance.CloseSettingMenu();
                }else{
                    if (UI_Manager.Instance.GetIsInAMenu()){
                        return;
                    }
                    UI_Manager.Instance.OpenSettingsMenu();
                }
            }
        }else{ // Main Menu Mode
            if (bOC_Settings){
                GameManager.Instance.LoadAScene(0);
            }

            if (bJump){
                if (UI_Manager.Instance.GetIsInSettingsMenu()){
                    UI_Manager.Instance.CloseSettingMenu();
                }else{
                    if (UI_Manager.Instance.GetIsInAMenu()){
                        return;
                    }
                    UI_Manager.Instance.OpenSettingsMenu();
                }
            }
        }
    }
    public void FixedUpdate(){
        if (GameManager.Instance.GetCurrentScene_Index() >= 0){
            v2_Movement = playerControls.Movement.Move.ReadValue<Vector2>();
            if (PlayerManager.Instance != null){
                PlayerManager.Instance.MovePlayer(v2_Movement);
            }
        }
    }

    public void LateUpdate(){
            bJump = false;
            bInteract = false;
            bSummonMirror = false;
            bDestroyMirror = false;
            bOC_Settings = false;   
    }
}
