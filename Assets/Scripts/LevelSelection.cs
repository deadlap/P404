using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class LevelSelection : MonoBehaviour
{
    public static event Action<int> ChangeLevelEvent;
    public static void OnChangeLevel(int level) => ChangeLevelEvent?.Invoke(level);

    // public static event Action NextLevelEvent;
    // public static void OnNextLevel() => NextLevelEvent?.Invoke();

    public static event Action ReloadLevelEvent;
    public static void OnReloadLevel() => ReloadLevelEvent?.Invoke();

    [SerializeField] List<GameObject> levels;
    [SerializeField] int levelCount; //which level we are on
    int currentLevel;
    Animator animator;
    bool isLoading;
    bool gameLoaded;
    
    void Awake()
    {
        levelCount = 0;
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        ReloadLevel();
    }

    void OnEnable()
    {
        ChangeLevelEvent += BeginLevelChange;
        ReloadLevelEvent += ReloadLevel;
        SceneManager.sceneLoaded += ReloadLevel;
    }

    void OnDisable()
    {
        ChangeLevelEvent -= BeginLevelChange;
        ReloadLevelEvent -= ReloadLevel;
        SceneManager.sceneLoaded -= ReloadLevel;
    }

    void ReloadLevel(Scene scene, LoadSceneMode mode)
    {
        BeginLevelChange(levelCount);
    }

    void ReloadLevel()
    {
        BeginLevelChange(levelCount);
    }

    void BeginLevelChange(int levelNumber)
    {
        if(isLoading) return;
        isLoading = true;
        currentLevel = levelNumber;
        if (levelCount < levelNumber || levelCount > levelNumber)
            levelCount = levelNumber;
        if (gameLoaded || levelCount != 0)
            animator.SetTrigger("FadeLevel");
        if (levelCount == 0)
            ChangeLevel();
    }

    public void ChangeLevel()       //accessed by animator
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        var key = GameObject.FindGameObjectWithTag("Key");
        if (key != null)
            GameObject.Destroy(key);

        var newObject = Instantiate(levels[currentLevel], gameObject.transform);
        newObject.SetActive(true);
        PlayerSpawnPoint.Instance.ResetPlayerPos();
        gameLoaded = true;
        isLoading = false;
    }
}