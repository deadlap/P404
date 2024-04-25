using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : MonoBehaviour {
    [SerializeField] bool portalActive;
    [SerializeField] bool doorActive;
    [SerializeField] string nextLevel;
    [SerializeField] string letterToOpen;
    [SerializeField] Animator animator;
    bool openDoor;

    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    void OnEnable() {
        ActivateableSigns.SpecialSignEvent += OpenDoor;
    }

    void OnDisable() {
        ActivateableSigns.SpecialSignEvent -= OpenDoor;
    }
    
    void Start() {
        audioSource = GetComponent<AudioSource>();
        portalActive = false;
        doorActive = false;
        openDoor = false;
    }
    void Update(){
        if (openDoor){
            openDoor = !openDoor;
            animator.SetTrigger("Open");
        }
    }
    void OpenDoor(string input){
        if (letterToOpen == input) {
            portalActive = true;
            openDoor = true;
            audioSource.PlayOneShot(audioClips[0]);
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
            audioSource.PlayOneShot(audioClips[1]);
            SceneChanger.OnChangeScene(nextLevel);
        }
    }
}
