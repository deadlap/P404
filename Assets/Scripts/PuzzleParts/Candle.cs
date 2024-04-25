using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour {
    
    [SerializeField] GameObject flame; //VFX flame thing
    public bool isBurning { get; private set; } //Whether the flame should be active or not
    [SerializeField] bool startingMode;

    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        isBurning = startingMode;
        ToggleFlame(isBurning);
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Water") && isBurning) {
            ToggleFlame(false);
            audioSource.PlayOneShot(audioClips[0]);
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
