using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpellSelfDeletion : MonoBehaviour {

    [SerializeField] float scaleRatio;
    [SerializeField] float deletionTime;
    [SerializeField] float scaleTime;
    float currentTime;

    void Start() {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update() {
        currentTime += Time.deltaTime;
        if (currentTime >= scaleTime) {
            this.transform.localScale *= scaleRatio;
        }
        if (currentTime >= deletionTime) {
            Destroy(this);
        }
    }
}
