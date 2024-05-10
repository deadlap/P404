using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Drawer1 : MonoBehaviour { //Should probably have been named hint drawer or something
    [SerializeField] string signToOpen;
    [SerializeField] Animator animator;
    [SerializeField] bool hasKey;
    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable() {
        ActivateableSigns.SpecialSignEvent += ReceiveSign;
    }

    void OnDisable() {
        ActivateableSigns.SpecialSignEvent -= ReceiveSign;
    }

    void ReceiveSign(string sign){
        if (sign == signToOpen) {
            animator.SetTrigger("Open");
            if (audioSource != null)
                audioSource.Play();
            if (hasKey) {}
                Key.OnActivatePickup();
        }
        HandModelGenerator.OnDeleteHandModel(signToOpen);
    }
}
