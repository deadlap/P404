using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerHint : MonoBehaviour
{
    [SerializeField] [TextArea] string[] hints;
    [SerializeField] TMP_Text hintText;
    int hintCount;
    float time;
    Coroutine coroutineTimer;
    [SerializeField] float timeBeforeHint;
    [SerializeField] float resetTime;

    void Start()
    {
        hintText.text = "";
    }

    public void PalmFacingPlayer()
    {
        print("palm facing player");
        time += Time.deltaTime;
        if (time > timeBeforeHint)
        {
            GiveHint();
            time = 0;
        }
        StartTimer(resetTime);
    }
    
    void GiveHint()
    {
        if (hintCount > hints.Length)
        {
            hintCount = 0;
        }
        hintText.text = hints[hintCount];
        hintCount++;
        ResetTimer();
    }
    
    void StartTimer(float time)
    {
        //Avoid having multiple coroutines running and deleting patterns all the time.
        if(coroutineTimer != null)
            StopCoroutine(coroutineTimer);
        coroutineTimer = StartCoroutine(TimeLimit(time));
    }
    
    IEnumerator TimeLimit(float time)
    {
        yield return new WaitForSeconds(time);
        ResetTimer();
    }

    void ResetTimer()
    {
        if(coroutineTimer != null)
            StopCoroutine(coroutineTimer);
        coroutineTimer = null;
    }
}
