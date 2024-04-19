using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateableSigns : MonoBehaviour {


    public void EnableSign(string sign){
        GameObject chosenGO = GameObject.Find(sign.ToUpper());
    }
}
