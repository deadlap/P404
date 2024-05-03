using System;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;
    Animator animator;
    bool isPlaying;
    void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void PlayAnimation()
    {
        if (!isPlaying)
        {
            animator.SetTrigger("FadeLevel");
            isPlaying = true;
        }
    }

    public void LoadLevel() //accessed by animator
    {
        LevelSelection.Instance.ChangeLevel();
        isPlaying = false;
    }
}
