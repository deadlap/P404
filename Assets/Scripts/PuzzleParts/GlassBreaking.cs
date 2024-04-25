using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBreaking : MonoBehaviour {
    [SerializeField] GameObject glass;
    [SerializeField] GameObject brokenGlass;
    [SerializeField] BoxCollider collider;
    [SerializeField] bool isBroken;
    
    void Start() {
        isBroken = false;
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Earth") && !isBroken) {
            collider.enabled = false;
            glass.SetActive(false);
            brokenGlass.SetActive(true);
            isBroken = true;
            Key.OnActivatePickup();
        }
    }
}
