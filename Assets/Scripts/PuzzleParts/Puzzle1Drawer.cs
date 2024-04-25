using UnityEngine;

public class Puzzle1Drawer : MonoBehaviour {
    [SerializeField] Candle candle;
    [SerializeField] Animator animator;
    bool hasPlayed;
    
    AudioSource audioSource;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        hasPlayed = false;
    }

    void Update() {
        if (!hasPlayed && candle.isBurning){
            hasPlayed = true;
            animator.SetTrigger("Open");
            audioSource.Play();
            Key.OnActivatePickup();
        }
    }
}
