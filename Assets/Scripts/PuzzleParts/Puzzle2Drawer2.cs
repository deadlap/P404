using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Drawer2 : MonoBehaviour {
    [SerializeField] Candle candle1;
    [SerializeField] Candle candle2;
    [SerializeField] Candle candle3;
    [SerializeField] Animator animator;
    bool hasPlayed;

    void Start() {
        hasPlayed = false;   
    }

    // Update is called once per frame
    void Update() {
        if (!hasPlayed && (candle1.isBurning && candle2.isBurning && !candle3.isBurning)){
            hasPlayed = true;
            animator.SetTrigger("Open");
            Key.OnActivatePickup();
        }
        
    }
}
