using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour {
    
    [SerializeField] GameObject flame; //VFX flame thing
    bool isBurning; //Whether the flame should be active or not

    void Start() {
        isBurning = false;
        ToggleFlame(isBurning);
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Water") && isBurning) {
            ToggleFlame(false);
        }
        if (other.CompareTag("Fire") && !isBurning) {
            ToggleFlame(true);
        }
    }

    void ToggleFlame(bool toggle){
        isBurning = toggle;
        flame.SetActive(isBurning);
    }
}
