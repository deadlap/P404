using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelSelection : MonoBehaviour {

    public static event Action<int> ChangeLevelEvent;
    public static void OnChangeLevel(int level) => ChangeLevelEvent?.Invoke(level);

    public static event Action NextLevelEvent;
    public static void OnNextLevel() => NextLevelEvent?.Invoke();

    [SerializeField] List<GameObject> levels;
    [SerializeField] int levelCount; //which level we are on

    void Start(){
        levelCount = 0;
    }

    void OnEnable() {
        NextLevelEvent += NextLevel;;
    }

    void OnDisable() {
        NextLevelEvent -= NextLevel;
    }

    void NextLevel(){
        levelCount++;
        ChangeLevel(levelCount);

    }

    void ChangeLevel(int levelNumber) {
        for (var i = 0; i < levels.Count; i++) {
            levels[i].SetActive(false);
            if (i == levelNumber)
                levels[i].SetActive(true);
        }
        PlayerSpawnPoint.Instance.ResetPlayerPos();
    }

}
