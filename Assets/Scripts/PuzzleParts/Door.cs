using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : MonoBehaviour {
    [SerializeField] bool portalActive;
    [SerializeField] bool doorActive;
    [SerializeField] string nextLevel;
    [SerializeField] string letterToOpen;
    // public static event Action OpenDoorEvent;
    // public static void OnOpenDoor() => OpenDoorEvent?.Invoke();
    
    void OnEnable() {
        ActivateableSigns.SpecialSignEvent += OpenDoor;
    }

    void OnDisable() {
        ActivateableSigns.SpecialSignEvent -= OpenDoor;
    }
    
    void Start(){
        portalActive = false;
        doorActive = false;
    }
    
    void OpenDoor(string input){
        if (letterToOpen == input) {
            portalActive = true;
            //play open door animation
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Key") && !doorActive) {
            Destroy(other.gameObject);
            doorActive = true;
            ActivateableSigns.OnEnableSpecialSign(letterToOpen);
        }
        if (other.CompareTag("FingerTip") && portalActive) {
            
            //Activate transition screen kode
            
            SceneChanger.OnChangeScene(nextLevel);
        }
    }
}
