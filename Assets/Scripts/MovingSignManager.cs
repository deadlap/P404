using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovingSignManager : MonoBehaviour
{
    public static MovingSignManager Instance;
    [SerializeField] List<GameObject> signPatterns;

    //[SerializeField] TMP_Text currentSign;
    
    GameObject åPattern;
    GameObject jPattern;
    GameObject øPattern;
    GameObject zPattern;
    
    GameObject signPattern;

    float timeLimit = 2f;

    Vector3 leftHandedRotation = new (0, 180, 0);

    TMP_Text recognisedSign;
    string prevRecognisedSign;

    string recognisedMovingSign;

    readonly int startForCheckPoints = 1;
    int checkPointReached;
    int patternChildCount;

    string handedness;


    Coroutine coroutineTimer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        recognisedSign = GameObject.Find("SignText").GetComponent<TMP_Text>();
        handedness = ChooseHand.Instance.handedness;
    }

    void Update()
    {
        if(recognisedSign == null) return;
        if(prevRecognisedSign == recognisedSign.text) return;
        DetectSign(recognisedSign.text);
    }

    void DetectSign(string sign)
    {
        prevRecognisedSign = recognisedSign.text;
        switch (sign)
        {
            case "A":
                if (signPattern)
                    DestroyPattern();
                SpawnPattern("Å", "Palm", 0); 
                break;
            case "I":
                if (signPattern)
                    DestroyPattern();
                SpawnPattern("J", "LittleTip", 1);
                break;
            case "O":
                if (signPattern)
                    DestroyPattern();
                SpawnPattern("Ø", "Palm", 2);
                break;
            case "X":
                if (signPattern)
                    DestroyPattern();
                SpawnPattern("Z", "IndexTip", 3);
                break;
            default:
                prevRecognisedSign = "";
                break;
        }
    }

    void SpawnPattern(string sign, string spawnpointString, int patternIndex)
    {
        checkPointReached = startForCheckPoints;
        recognisedMovingSign = sign;

        var spawnpoint = GameObject.Find($"{handedness}_{spawnpointString}");
        transform.parent = spawnpoint.transform;
        transform.position = spawnpoint.transform.position;
                    
        switch (handedness)
        {
            case "L":
                signPattern = Instantiate(signPatterns[patternIndex], spawnpoint.transform.position, Quaternion.Euler(leftHandedRotation));
                break;
            case "R":
                signPattern = Instantiate(signPatterns[patternIndex], spawnpoint.transform.position, Quaternion.identity);
                break;
        }
        patternChildCount = signPatterns[patternIndex].transform.childCount;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == checkPointReached.ToString())
        {
            SignCompleted();
        }
        
        //Avoid fail when signing "Å", since the last checkpoint is at the start as well.
        if(recognisedMovingSign == "Å" || checkPointReached == 2) return; 
        if (int.TryParse(other.gameObject.name, out var checkPointName) && checkPointName > checkPointReached)
        {
            MovingSignFailed();
        }
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
        DestroyPattern();
    }
    
    void SignCompleted()
    {
        Destroy(GameObject.Find(checkPointReached.ToString()));
        StartTimer(timeLimit);
        if (checkPointReached >= patternChildCount)
        {
            recognisedSign.text = recognisedMovingSign;
            print($"{recognisedMovingSign}");
            DestroyPattern();
        }
        checkPointReached++;
    }

    //debug funktion - ersat med DestroyPattern()
    void MovingSignFailed()
    {
        DestroyPattern();
        print("YOU FAILED BITCH");
    }
    
    void DestroyPattern()
    {
        
        if(coroutineTimer != null)
            StopCoroutine(coroutineTimer);
        coroutineTimer = null;
        Destroy(signPattern);
    }
}
