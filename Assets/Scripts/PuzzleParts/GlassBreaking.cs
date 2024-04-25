using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBreaking : MonoBehaviour {
    [SerializeField] GameObject glass;
    [SerializeField] GameObject brokenGlass;
    [SerializeField] BoxCollider collider;
    [SerializeField] bool isBroken;

    AudioSource audioSource;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        isBroken = false;
    }

    void OnTriggerEnter(Collider other){
        SpellingCheck.OnDeleteSpells();
        if (other.CompareTag("Earth") && !isBroken) {
            collider.enabled = false;
            glass.SetActive(false);
            brokenGlass.SetActive(true);
            isBroken = true;
            audioSource.Play();
            Key.OnActivatePickup();
        }
    }
}
