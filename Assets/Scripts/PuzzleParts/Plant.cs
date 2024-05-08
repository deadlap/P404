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
            burnt = true;
        }
    }

    void Grow(){
        if (!burnt && !grown){
            SpellingCheck.OnDeleteSpells();
            animator.SetTrigger("Grow");
            grown = true;
        }
    }
}
