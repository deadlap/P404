using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class SceneChanger : MonoBehaviour {

    public static event Action<string> ChangeSceneEvent;
    public static void OnChangeScene(string scene) => ChangeSceneEvent?.Invoke(scene);

    void OnEnable() {
        ChangeSceneEvent += ChangeScene;;
    }

    void OnDisable() {
        ChangeSceneEvent -= ChangeScene;
    }

    void ChangeScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    
}