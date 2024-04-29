using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
public class LevelSelection : MonoBehaviour {

    public static event Action<int> ChangeLevelEvent;
    public static void OnChangeLevel(int level) => ChangeLevelEvent?.Invoke(level);

    public static event Action NextLevelEvent;
    public static void OnNextLevel() => NextLevelEvent?.Invoke();

    [SerializeField] List<GameObject> levels;
    [SerializeField] int levelCount; //which level we are on

    void Awake(){
        levelCount = 0;
    }

    void Start(){
        ReloadLevel();
    }

    void OnEnable() {
        NextLevelEvent += NextLevel;;
        SceneManager.sceneLoaded += ReloadLevel;
    }

    void OnDisable() {
        NextLevelEvent -= NextLevel;
       SceneManager.sceneLoaded -= ReloadLevel;
    }

    void ReloadLevel(Scene scene, LoadSceneMode mode){
        ChangeLevel(levelCount);
    }
    void ReloadLevel(){
        ChangeLevel(levelCount);
    }

    void NextLevel(){
        levelCount++;
        ChangeLevel(levelCount);
    }

    void ChangeLevel(int levelNumber) {
        foreach (Transform child in gameObject.transform) {
	        GameObject.Destroy(child.gameObject);
        }
        var newObject = Instantiate(levels[levelNumber], gameObject.transform);
        newObject.SetActive(true);
        PlayerSpawnPoint.Instance.ResetPlayerPos();
    }

}
