using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Plant : MonoBehaviour {
    public bool burnt { get; private set; }
    public bool grown { get; private set; }
    [SerializeField] Animator animator;
    public static event Action GrowPlantEvent;
    public static void OnGrowPlant() => GrowPlantEvent?.Invoke();
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;
    [SerializeField] Glass2 glass;
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable() {
        GrowPlantEvent += Grow;
    }
    void OnDisable() {
        GrowPlantEvent -= Grow;
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Fire") && grown && !burnt) {
            SpellingCheck.OnDeleteSpells();
            animator.SetTrigger("Burn");
            audioSource.PlayOneShot(audioClips[1]);
            burnt = true;
        }
    }

    void Grow(){
        if (!burnt && !grown && glass.isBroken){
            SpellingCheck.OnDeleteSpells();
            animator.SetTrigger("Grow");
            audioSource.PlayOneShot(audioClips[0]);
            grown = true;
        }
    }
}
