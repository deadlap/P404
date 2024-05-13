using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class LevelSelection : MonoBehaviour
{
    public static LevelSelection Instance;
    public static event Action<int> ChangeLevelEvent;
    public static void OnChangeLevel(int level) => ChangeLevelEvent?.Invoke(level);
    [SerializeField] GameObject restartText;
    [SerializeField] GameObject restartLevel;

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
        Instance = this;
        levelCount = 0;
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        //ReloadLevel();
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
        currentLevel = levelNumber;
        if (levelCount < levelNumber || levelCount > levelNumber)
            levelCount = levelNumber;
        if (gameLoaded || levelNumber > 0) 
            LoadingScreen.Instance.PlayAnimation();
        if (!gameLoaded)
            ChangeLevel();
    }

    public void ChangeLevel()
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
        if (levelCount == 0) {
            restartText.SetActive(false);
            restartLevel.SetActive(false);
        } else {
            restartText.SetActive(true);
            restartLevel.SetActive(true);
        }
    }
}