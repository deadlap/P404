using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {
    [SerializeField] Animator animator;
    public bool IsOpen { get; private set; }
    void Start(){
        IsOpen = false;
    }
    void OnEnable() {
        TeslaCoil.ActivateTeslaCoilEvent += OpenGate;
    }

    void OnDisable()
    {
        TeslaCoil.ActivateTeslaCoilEvent -= OpenGate;
    }

    // Update is called once per frame
    void OpenGate(string thing){
        IsOpen = true;
        animator.SetTrigger("Activate");
    }
}
