using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandModelGenerator : MonoBehaviour {
    public static event Action<string> CreateHandModelEvent;
    public static void OnCreateHandModel(string sign) => CreateHandModelEvent?.Invoke(sign);
    public static event Action<string> DeleteHandModelEvent;
    public static void OnDeleteHandModel(string sign) => DeleteHandModelEvent?.Invoke(sign);
    [SerializeField] string signToGenerate;

    void OnEnable() {
        CreateHandModelEvent += GenerateHandModel;
        DeleteHandModelEvent += DeleteHandModels;
    }

    void OnDisable() {
        CreateHandModelEvent -= GenerateHandModel;
        DeleteHandModelEvent -= DeleteHandModels;
    }
    public void GenerateHandModel(string sign) {
        if (sign == signToGenerate && this.transform.childCount < 1) {
            GameObject gameobject = Instantiate(Resources.Load("Models/Handshapes/"+sign.ToUpper() + "_Handshape"), this.transform) as GameObject;
        }
    }

    void DeleteHandModels(string sign){
        if (sign == signToGenerate) {
            foreach (Transform child in this.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
