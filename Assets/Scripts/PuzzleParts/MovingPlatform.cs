using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    bool activated;
    [SerializeField] Animator animator;
    void Start() {
        activated = false;
    }
    void OnEnable() {
        TeslaCoil.ActivateTeslaCoilEvent += ActivatePlatform;
    }
    void OnDisable() {
        TeslaCoil.ActivateTeslaCoilEvent -= ActivatePlatform;
    }
    void ActivatePlatform(string _){
        animator.SetTrigger("Activate");
    }
}
