using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovingSign : MonoBehaviour
{
    [SerializeField] List<GameObject> signPatterns;

    [SerializeField] TMP_Text currentSign;
    
    GameObject åPattern;
    GameObject jPattern;
    GameObject øPattern;
    GameObject zPattern; 
    
    [SerializeField] string recognisedSign;
    string prevRecognisedSign;

    string recognisedMovingSign;

    readonly int startForCheckPoints = 1;
    int checkPointReached;
    int patternChildCount;

    string handedness;
    void Start()
    {
        handedness = ChooseHand.Instance.handedness;
        switch (handedness)
        {
            case "L":
                currentSign = GameObject.Find("LeftText").GetComponent<TMP_Text>();
                break;
            case "R":
                currentSign = GameObject.Find("RightText").GetComponent<TMP_Text>();
                break;
        }
    }

    void Update()
    {
        if(prevRecognisedSign == recognisedSign) return;
        SpawnSignPattern(recognisedSign);
    }

    void SpawnSignPattern(string sign)
    {
        prevRecognisedSign = recognisedSign;
        checkPointReached = startForCheckPoints;
        switch (sign)
        {
            case "A":
                if (!åPattern)
                {
                    recognisedMovingSign = "Å"; 
                    DestroyAllPatterns();
                    åPattern = Instantiate(signPatterns[0], gameObject.transform.position, Quaternion.identity);
                    patternChildCount = signPatterns[0].transform.childCount;
                }
                break;
            case "I":
                if (!jPattern)
                {
                    recognisedMovingSign = "J";
                    DestroyAllPatterns();
                    GameObject spawnpoint = GameObject.Find($"{handedness}_LittleTip");
                    jPattern = Instantiate(signPatterns[1], spawnpoint.transform.position, Quaternion.identity, spawnpoint.transform);
                    patternChildCount = signPatterns[1].transform.childCount;
                }
                break;
            case "O":
                if (!øPattern)
                {
                    recognisedMovingSign = "Ø";
                    DestroyAllPatterns();
                    øPattern = Instantiate(signPatterns[2], gameObject.transform.position, Quaternion.identity);
                    patternChildCount = signPatterns[2].transform.childCount;
                }
                break;
            case "X":
                if (!zPattern)
                {
                    recognisedMovingSign = "Z";
                    DestroyAllPatterns();
                    GameObject spawnpoint = GameObject.Find($"{handedness}_IndexTip");
                    zPattern = Instantiate(signPatterns[3], spawnpoint.transform.position, Quaternion.identity, spawnpoint.transform);
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
            currentSign.text = recognisedSign;
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
