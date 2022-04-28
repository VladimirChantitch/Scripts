using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip doorMoving;
    [SerializeField] private GameObject leftPanel;
    [SerializeField] private GameObject rightPanel;
    [SerializeField] private float offSet = 2.5f;
    [SerializeField] private float openingSpeed = 5f;
    Vector3 leftTarget;
    Vector3 righttarget;
    Vector3 intialRightPosition;
    Vector3 inititalLeftPosition;
    bool bCanOpen;
    void Start(){
        inititalLeftPosition = leftPanel.transform.localPosition;
        intialRightPosition = rightPanel.transform.localPosition;

        leftTarget = new Vector3(leftPanel.transform.localPosition.x, leftPanel.transform.localPosition.y, leftPanel.transform.localPosition.z -offSet);
        righttarget = new Vector3(rightPanel.transform.localPosition.x, rightPanel.transform.localPosition.y, rightPanel.transform.localPosition.z +offSet);
    }

    public void SetCanOpen(bool canIt){
        if (bCanOpen == canIt){
            return;
        }
        audioSource.PlayOneShot(doorMoving);
        bCanOpen = canIt;
    }
    void Update(){
        if (bCanOpen){
            leftPanel.transform.localPosition = Vector3.Lerp(leftPanel.transform.localPosition, leftTarget, Time.deltaTime * openingSpeed);
            rightPanel.transform.localPosition = Vector3.Lerp(rightPanel.transform.localPosition, righttarget, Time.deltaTime * openingSpeed);
        }else{
            leftPanel.transform.localPosition = Vector3.Lerp(leftPanel.transform.localPosition, inititalLeftPosition, Time.deltaTime * openingSpeed * 2);
            rightPanel.transform.localPosition = Vector3.Lerp(rightPanel.transform.localPosition, intialRightPosition, Time.deltaTime * openingSpeed * 2);
        }
    }
}
