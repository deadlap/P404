using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class SpecialSignActivation : MonoBehaviour {
    [SerializeField] string signToActivate;

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("FingerTip")){
            ActivateableSigns.OnEnableSpecialSign(signToActivate);
        }
    }
}
