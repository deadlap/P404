using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatLvl6 : MonoBehaviour {
    [SerializeField] Candle candle;
    public Animator animator;
    public bool IsSailing;
    void Update() {
        animator.SetBool("fire", candle.isBurning);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Wind") && !IsSailing) {
            SpellingCheck.OnDeleteSpells();
            animator.SetTrigger("Activate");
        }
    }
}
