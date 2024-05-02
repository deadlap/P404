using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHandednessScale : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        var scaleVector = transform.localScale;
        if (transform.parent.parent.name == "SpawnHandPosition") {
            transform.localScale = new Vector3(
                scaleVector.x*ChooseHand.Instance.handednessScale, scaleVector.y, scaleVector.z);
        } else {
            transform.localScale = new Vector3(
                scaleVector.x, scaleVector.y*ChooseHand.Instance.handednessScale, scaleVector.z);
        }
    }
}
