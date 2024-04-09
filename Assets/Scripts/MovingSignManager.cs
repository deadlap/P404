using System;
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

    float timeLimit = 5f;
    float time = 0;

    Vector3 leftHandedRotation = new (0, 180, 0);
    
    [SerializeField] TMP_Text recognisedSign;
    string prevRecognisedSign;

    string recognisedMovingSign;

    readonly int startForCheckPoints = 1;
    int checkPointReached;
    int patternChildCount;

    string handedness;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        handedness = ChooseHand.Instance.handedness;
        switch (handedness)
        {
            case "L":
                recognisedSign = GameObject.Find("LeftText").GetComponent<TMP_Text>();
                break;
            case "R":
                recognisedSign = GameObject.Find("RightText").GetComponent<TMP_Text>();
                break;
        }
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
        checkPointReached = startForCheckPoints;
        switch (sign)
        {
            case "A":
                if (!åPattern)
                {
                    time = 0;
                    DestroyAllPatterns();
                    recognisedMovingSign = "Å"; 
                    
                    transform.parent = GameObject.Find($"{handedness}_Palm").transform;
                    transform.position = GameObject.Find($"{handedness}_Palm").transform.position;

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
                    time = 0;
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
                    time = 0;
                    DestroyAllPatterns();
                    recognisedMovingSign = "Ø";
                    
                    transform.parent = GameObject.Find($"{handedness}_Palm").transform;
                    transform.position = GameObject.Find($"{handedness}_Palm").transform.position;
                    
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
                    time = 0;
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
                DestroyAllPatterns();
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
        //please dont look, im sorry
        if (other.gameObject.name == (checkPointReached + 1).ToString())
        {
            MovingSignFailed();
        }
        //please dont look, im sorry
        if (other.gameObject.name == (checkPointReached + 2).ToString())
        {
            MovingSignFailed();
        }
    }

    void SignCompleted()
    {
        Destroy(GameObject.Find(checkPointReached.ToString()));
        if (checkPointReached == patternChildCount)
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
        Destroy(åPattern);
        Destroy(jPattern);
        Destroy(øPattern);
        Destroy(zPattern);
    }
}
