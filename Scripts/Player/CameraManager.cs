using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera c_MainCamera;
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    public void SetSensivity(float sens){
        sensX = sens;
        sensY = sens;
    }
    public float GetSensitivity(){
        return sensX;
    }
    [SerializeField] private Transform orientation;
    float yRotation;
    float xRotation;
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MoveCameraOnMousePosition(float mouseX, float mouseY){
        if (UI_Manager.Instance.GetIsInAMenu()){
            return;
        }

        float mouseMoveX = mouseX * Time.deltaTime * sensX;
        float mouseMoveY = mouseY * Time.deltaTime * sensY;

        yRotation += mouseMoveX;
        xRotation -= mouseMoveY;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        c_MainCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation,0);
    }
}
