using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour {
    
    [SerializeField] GameObject flame; //VFX flame thing
    public bool isBurning { get; private set; } //Whether the flame should be active or not
    [SerializeField] bool startingMode;
    
    void Start() {
        isBurning = startingMode;
        ToggleFlame(isBurning);
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Water") && isBurning) {
            ToggleFlame(false);
            SpellingCheck.OnDeleteSpells();
        }
        if (other.CompareTag("Fire") && !isBurning) {
            ToggleFlame(true);
            SpellingCheck.OnDeleteSpells();
        }
    }

    void ToggleFlame(bool toggle){
        isBurning = toggle;
        flame.SetActive(isBurning);
    }
}
