using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainPortal : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("FingerTip")) {
            
            SceneChanger.OnChangeScene("Level1");
        }
    }
}
