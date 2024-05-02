using System;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;
    Animator animator;
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
        animator.SetTrigger("FadeLevel");
    }

    public void LoadLevel() //accessed by animator
    {
        LevelSelection.Instance.ChangeLevel();
    }
}
