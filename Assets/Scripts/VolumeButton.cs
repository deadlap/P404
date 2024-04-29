using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeButton : MonoBehaviour {
    [SerializeField] string message;
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("FingerTip")){
            SoundHandler.Instance.ChangeText(message);
        }
    }
}
