using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class SceneChanger : MonoBehaviour {

    public static event Action<string> ChangeSceneEvent;
    public static void OnChangeScene(string scene) => ChangeSceneEvent?.Invoke(scene);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReloadScene();
        }
    }

    void OnEnable() {
        ChangeSceneEvent += ChangeScene;;
    }

    void OnDisable() {
        ChangeSceneEvent -= ChangeScene;
    }

    void ChangeScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}