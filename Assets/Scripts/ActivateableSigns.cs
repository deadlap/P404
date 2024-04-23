using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActivateableSigns : MonoBehaviour {
    public static event Action<string> SpecialSignEvent;
    public static void OnSpecialSign(string sign) => SpecialSignEvent?.Invoke(sign);

    public static event Action<string> ReceiveSpecialSignEvent;
    public static void ReceiveSpecialSign(string sign) => ReceiveSpecialSignEvent?.Invoke(sign);

    public static event Action<string> EnableSpecialSignEvent;
    public static void OnEnableSpecialSign(string sign) => EnableSpecialSignEvent?.Invoke(sign);

    [SerializeField] public GameObject movingSignManager;
    [SerializeField] string currentSign;
    void OnEnable() {
        ReceiveSpecialSignEvent += ReceiveSign;
        EnableSpecialSignEvent += EnableSign;
    }

    void OnDisable() {
        ReceiveSpecialSignEvent -= ReceiveSign;
        EnableSpecialSignEvent -= EnableSign;
    }

    public void EnableSign(string sign) {
        currentSign = sign;
        string movingSigns = "JZØÅ";
        print("hej"); print(movingSigns.Contains(sign));
        if (movingSigns.Contains(sign)) {
            this.transform.Find(sign.ToUpper()).gameObject.SetActive(true);
            movingSignManager.SetActive(true);
            HandModelGenerator.OnCreateHandModel(sign);
        }
    }

    public void ReceiveSign(string sign) {
        if (currentSign != sign)
            return;
        print("succesfully signed special: " + sign);
        foreach (Transform child in this.transform) {
	        child.gameObject.SetActive(false);
        }
        OnSpecialSign(sign.ToUpper());
        movingSignManager.SetActive(false);
        HandModelGenerator.OnDeleteHandModel(sign);
    }
}