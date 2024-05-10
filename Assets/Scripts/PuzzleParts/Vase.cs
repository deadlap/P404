using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour {
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Water")) {
            Plant.OnGrowPlant();
        }
    }
}