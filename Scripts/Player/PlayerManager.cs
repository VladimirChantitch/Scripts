using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set;}
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private PlayerMovementAndAnimation playerMovementAndAnimation;
    [SerializeField] private PlaceMirror placeMirror;
    [Header("Interact")]
    [SerializeField] private Transform placeOrigin;
    [SerializeField] private float interactRadius;
    [SerializeField] LayerMask whatCanInteract;
    private bool bCanMove;
    public bool CanMove(){return bCanMove;}

    [Header("sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip interactClip;

    //getter
    public float GetSensitivity(){
        float sens = cameraManager.GetSensitivity();
        return sens;
    }

    void Awake(){
        Instance = this;
        cameraManager = GetComponent<CameraManager>();
        playerInputs = GetComponent<PlayerInputs>();
        playerMovementAndAnimation = GetComponent<PlayerMovementAndAnimation>();
        placeMirror = GetComponent<PlaceMirror>();
    }
    void Update(){
        playerMovementAndAnimation.TickUpdate();
    }
    public void MoveCamera(float mouseX, float mouseY){
        cameraManager.MoveCameraOnMousePosition(mouseX, mouseY);
    }
    public void MovePlayer(Vector2 v2_Movement){
        if(playerMovementAndAnimation != null){
            playerMovementAndAnimation.MovePlayer(v2_Movement);
        }
    }
    public void TryToJump(){
        playerMovementAndAnimation.Jump();
    }
    public void TryPlacingMirror(){
        placeMirror.SpawnAMirror();
    }
    public void TryDestroyMirror(){
        placeMirror.DestroyMirror();
    }

    public void TryInteract(){
        Collider[] colliders = Physics.OverlapSphere(placeOrigin.transform.position, interactRadius, whatCanInteract);
        for (int i = 0; i < colliders.Length; i++){
            if (colliders[i] != null){
                colliders[i].TryGetComponent<Cadenas>(out Cadenas cadenas);
                if (cadenas != null){
                    UI_Manager.Instance.OpenCadenasMenu(cadenas);
                    audioSource.PlayOneShot(interactClip);
                }
            }
        }
    }

    public void HandleDeath(){
        placeMirror.DestroyAllMirrors();
    }

    public void SetSensivity(float sens){
        cameraManager.SetSensivity(sens);
    }
}
