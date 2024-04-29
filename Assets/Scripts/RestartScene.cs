using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class RestartScene : MonoBehaviour {

    void ReloadScene() { //Does not actually reload scene but calls reload level
        LevelSelection.OnReloadLevel();
        // var currentScene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(currentScene.name);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("FingerTip")) {
            ReloadScene();
        }
    }
}
