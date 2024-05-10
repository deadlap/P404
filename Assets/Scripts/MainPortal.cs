using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainPortal : MonoBehaviour
{
    AudioSource audioSource;
    bool usingPortal;
    [SerializeField] int nextLevel;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("NonDominantHand") || other.CompareTag("DominantHand") && !usingPortal) {
            audioSource.Play();
            LevelSelection.OnChangeLevel(nextLevel);
            usingPortal = true;
        }
    }
}
