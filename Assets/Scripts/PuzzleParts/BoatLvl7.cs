using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatLvl7 : BoatLvl6 {
    [SerializeField] Gate gate;
    void Update() {
        animator.SetBool("GateOpen", gate.IsOpen);
    }
}
