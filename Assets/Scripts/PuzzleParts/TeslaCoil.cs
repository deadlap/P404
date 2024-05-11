using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeslaCoil : MonoBehaviour {
    
    public static event Action<string> ActivateTeslaCoilEvent;
    public static void OnActivateTeslaCoil(string tCTag) => ActivateTeslaCoilEvent?.Invoke(tCTag);
    
    [SerializeField] string teslaCoilTag; //Is only used if we ever want more than 1 tesla coil pr level
    [SerializeField] Animator animator;
    void OnTriggerEnter(Collider other){
        
        if (other.CompareTag("Lightning")) {
            OnActivateTeslaCoil(teslaCoilTag);
            animator.SetTrigger("Activate");
            SpellingCheck.OnDeleteSpells();
        }
    }
}
