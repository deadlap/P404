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
        SpawnSignPattern(recognisedSign.text);
    }

    void SpawnSignPattern(string sign)
    {
        prevRecognisedSign = recognisedSign.text;
        switch (sign)
        {
            case "A":
                if (!åPattern)
                {
                    checkPointReached = startForCheckPoints;
                    DestroyAllPatterns();
                    recognisedMovingSign = "Å"; 
                    
                    GameObject spawnpoint = GameObject.Find($"{handedness}_Palm");
                    transform.parent = spawnpoint.transform;
                    transform.position = spawnpoint.transform.position;

                    switch (handedness)
                    {
                        case "L":
                            åPattern = Instantiate(signPatterns[0], transform.position, Quaternion.Euler(leftHandedRotation));
                            break;
                        case "R":
                            åPattern = Instantiate(signPatterns[0], transform.position, Quaternion.identity);
                            break;
                    }
                    patternChildCount = signPatterns[0].transform.childCount;
                }
                break;
            case "I":
                if (!jPattern)
                {
                    checkPointReached = startForCheckPoints;
                    DestroyAllPatterns();
                    recognisedMovingSign = "J";
                    
                    GameObject spawnpoint = GameObject.Find($"{handedness}_LittleTip");
                    transform.parent = spawnpoint.transform;
                    transform.position = spawnpoint.transform.position;
                    
                    switch (handedness)
                    {
                        case "L":
                            jPattern = Instantiate(signPatterns[1], spawnpoint.transform.position, Quaternion.Euler(leftHandedRotation));
                            break;
                        case "R":
                            jPattern = Instantiate(signPatterns[1], spawnpoint.transform.position, Quaternion.identity);
                            break;
                    }
                    patternChildCount = signPatterns[1].transform.childCount;
                }
                break;
            case "O":
                if (!øPattern)
                {
                    checkPointReached = startForCheckPoints;
                    DestroyAllPatterns();
                    recognisedMovingSign = "Ø";
                    
                    GameObject spawnpoint = GameObject.Find($"{handedness}_Palm");
                    transform.parent = spawnpoint.transform;
                    transform.position = spawnpoint.transform.position;
                    
                    switch (handedness)
                    {
                        case "L":
                            øPattern = Instantiate(signPatterns[2], gameObject.transform.position, Quaternion.Euler(leftHandedRotation));
                            break;
                        case "R":
                            øPattern = Instantiate(signPatterns[2], gameObject.transform.position, Quaternion.identity);
                            break;
                    }
                    patternChildCount = signPatterns[2].transform.childCount;
                }
                break;
            case "X":
                if (!zPattern)
                {
                    checkPointReached = startForCheckPoints;
                    DestroyAllPatterns();
                    recognisedMovingSign = "Z";
                    
                    GameObject spawnpoint = GameObject.Find($"{handedness}_IndexTip");
                    transform.parent = spawnpoint.transform;
                    transform.position = spawnpoint.transform.position;
                    
                    switch (handedness)
                    {
                        case "L":
                            zPattern = Instantiate(signPatterns[3], spawnpoint.transform.position, Quaternion.Euler(leftHandedRotation));
                            break;
                        case "R":
                            zPattern = Instantiate(signPatterns[3], spawnpoint.transform.position, Quaternion.identity);
                            break;
                    }
                    patternChildCount = signPatterns[3].transform.childCount;
                }
                break;
            default:
                prevRecognisedSign = "";
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == checkPointReached.ToString())
        {
            SignCompleted();
        }

        if(recognisedMovingSign == "Å" || checkPointReached == 2) return;
        if (int.TryParse(other.gameObject.name, out var checkPointName) && checkPointName > checkPointReached)
        {
            MovingSignFailed();
        }
        
    //     //please dont look, im sorry
    //     if (other.gameObject.name == (checkPointReached + 1).ToString())
    //     {
    //         MovingSignFailed();
    //     }
    //     //please dont look, im sorry
    //     if (other.gameObject.name == (checkPointReached + 2).ToString())
    //     {
    //         MovingSignFailed();
    //     }
    }

    void StartTimer(float time)
    {
        if(coroutineTimer != null)
            StopCoroutine(coroutineTimer);
        coroutineTimer = StartCoroutine(TimeLimit(time));
    }
    
    IEnumerator TimeLimit(float time)
    {
        yield return new WaitForSeconds(time);
        print("time up");
        DestroyAllPatterns();
    }
    
    void SignCompleted()
    {
        Destroy(GameObject.Find(checkPointReached.ToString()));
        StartTimer(timeLimit);
        if (checkPointReached >= patternChildCount)
        {
            recognisedSign.text = recognisedMovingSign;
            print($"{recognisedMovingSign}");
            DestroyAllPatterns();
        }
        checkPointReached++;
    }

    void MovingSignFailed()
    {
        DestroyAllPatterns();
        print("YOU FAILED BITCH");
    }
    
    void DestroyAllPatterns()
    {
        if(coroutineTimer != null)
            StopCoroutine(coroutineTimer);
        coroutineTimer = null;
        Destroy(åPattern);
        Destroy(jPattern);
        Destroy(øPattern);
        Destroy(zPattern);
    }
}
