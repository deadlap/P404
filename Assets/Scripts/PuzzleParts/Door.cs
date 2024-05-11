using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] bool portalActive;
    [SerializeField] bool doorActive;
    [SerializeField] int nextLevel;
    [SerializeField] string letterToOpen;
    [SerializeField] Animator animator;
    bool openDoor;
    bool usingPortal;
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    void OnEnable() {
        ActivateableSigns.SpecialSignEvent += OpenDoor;
    }

    void OnDisable() {
        ActivateableSigns.SpecialSignEvent -= OpenDoor;
    }
    
    void Start() {
        audioSource = GetComponent<AudioSource>();
        portalActive = false;
        openDoor = false;
    }
    void Update(){
        if (openDoor){
            openDoor = !openDoor;
            animator.SetTrigger("Open");
        }
    }
    void OpenDoor(string input){
        if (letterToOpen == input) {
            portalActive = true;
            openDoor = true;
            audioSource.PlayOneShot(audioClips[0]);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Key")) {
            Destroy(other.gameObject);
            ActivateableSigns.OnEnableSpecialSign(letterToOpen);
        }
        if ((other.CompareTag("NonDominantHand") || other.CompareTag("DominantHand")) && portalActive && !usingPortal) {
            
            //Activate transition screen kode
            audioSource.PlayOneShot(audioClips[1]);
            LevelSelection.OnChangeLevel(nextLevel);
            usingPortal = true;
        }
    }
}
