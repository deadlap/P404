using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Drawer : MonoBehaviour {
    [SerializeField] Candle candle;
    [SerializeField] Animator animator;
    bool hasPlayed;

    void Start() {
        hasPlayed = false;
    }

    void Update() {
        if (!hasPlayed && candle.isBurning){
            hasPlayed = true;
            animator.SetTrigger("Open");
            Key.OnActivatePickup();
        }
    }
}
