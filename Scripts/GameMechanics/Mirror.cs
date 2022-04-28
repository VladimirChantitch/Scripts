using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour{
    [SerializeField] private Camera mirrorCamera;
    public Camera GetCamera(){
        return mirrorCamera;
    }
    [SerializeField] private GameObject mirrorMesh;
    public GameObject GetMirrorMesh(){
        return mirrorMesh;
    }
}