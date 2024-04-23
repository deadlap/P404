using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class SpecialSignActivation : MonoBehaviour {
    [SerializeField] string signToActivate;
    [SerializeField] TMP_Text specialSignText;
    
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("FingerTip")){
            ActivateableSigns.OnEnableSpecialSign(signToActivate);
            specialSignText.text = signToActivate;
        }
    }
}
