using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainPortal : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("FingerTip")) {

            audioSource.Play();
            LevelSelection.OnChangeLevel(1);
            // SceneChanger.OnChangeScene("Level1");
        }
    }
}
