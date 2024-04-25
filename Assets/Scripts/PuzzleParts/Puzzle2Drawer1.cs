using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Drawer1 : MonoBehaviour {
    [SerializeField] string signToOpen;
    [SerializeField] Animator animator;
    
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable() {
        ActivateableSigns.SpecialSignEvent += ReceiveSign;
    }

    void OnDisable() {
        ActivateableSigns.SpecialSignEvent -= ReceiveSign;
    }

    void Update() {
        
    }

    void ReceiveSign(string sign){
        if (sign == signToOpen) {
            animator.SetTrigger("Open");
            audioSource.Play();
        }
        HandModelGenerator.OnDeleteHandModel(signToOpen);
    }
}
