using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ActivateableSigns : MonoBehaviour {
    public static event Action<string> SpecialSignEvent;
    public static void OnSpecialSign(string sign) => SpecialSignEvent?.Invoke(sign);

    public static event Action<string> ReceiveSpecialSignEvent;
    public static void ReceiveSpecialSign(string sign) => ReceiveSpecialSignEvent?.Invoke(sign);

    public static event Action<string> EnableSpecialSignEvent;
    public static void OnEnableSpecialSign(string sign) => EnableSpecialSignEvent?.Invoke(sign);

    [SerializeField] public GameObject movingSignManager;
    [SerializeField] string currentSign;
    [SerializeField] TMP_Text specialSignedText;
    void OnEnable() {
        ReceiveSpecialSignEvent += ReceiveSign;
        EnableSpecialSignEvent += EnableSign;
    }

    void OnDisable() {
        ReceiveSpecialSignEvent -= ReceiveSign;
        EnableSpecialSignEvent -= EnableSign;
    }

    void Update(){
        ReceiveSign(specialSignedText.text.ToUpper());
    }

    public void EnableSign(string sign) {
        currentSign = sign;
        string movingSigns = "JZØÅ";
        if (movingSigns.Contains(sign)) {
            movingSignManager.SetActive(true);
        }
        this.transform.Find(sign.ToUpper()).gameObject.SetActive(true);
        HandModelGenerator.OnCreateHandModel(sign);
    }

    public void ReceiveSign(string sign) {
        print("registrere: " + sign);
        if(specialSignedText.text.Length == 0 || currentSign.Length == 0) return;
        if (currentSign != sign) return;
        print("succesfully signed special: " + sign);
        foreach (Transform child in this.transform) {
	        child.gameObject.SetActive(false);
        }
        OnSpecialSign(sign.ToUpper());
        movingSignManager.SetActive(false);
        HandModelGenerator.OnDeleteHandModel(sign);
    }
}