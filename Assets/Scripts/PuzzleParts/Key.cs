using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Key : MonoBehaviour {

    [SerializeField] bool pickedUp;
    [SerializeField] bool canBePickedUp;
    [SerializeField] Vector3 pickupPosition;
    [SerializeField] Vector3 scaleOnPickup;

    public static event Action ActivatePickup;
    public static void OnActivatePickup() => ActivatePickup?.Invoke();
    
    void OnEnable() {
        ActivatePickup += MakeInteractible;;
    }

    void OnDisable() {
        ActivatePickup -= MakeInteractible;
    }

    // void Start() {
    //     pickedUp = false;
    //     canBePickedUp = false;
    // }

    void MakeInteractible(){
        canBePickedUp = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("DominantHand") && canBePickedUp && !pickedUp) {
            pickedUp = true;
            // gameObject.transform.position = pickupPosition;
            // gameObject.transform.SetParent(other.transform);
            var copy = Instantiate(gameObject, other.gameObject.transform, true);
            copy.transform.localScale = scaleOnPickup;
            Destroy(gameObject);

        }
    }
}
