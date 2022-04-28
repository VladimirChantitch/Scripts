using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour{
    [SerializeField] DoorScript door;

    void OnTriggerEnter(Collider other){
        door.SetCanOpen(false);
    }
}
