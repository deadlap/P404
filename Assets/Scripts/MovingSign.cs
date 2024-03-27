using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MovingSign : MonoBehaviour
{
    [SerializeField] List<GameObject> signPatterns;

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
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if(prevRecognisedSign == recognisedSign) return;
        SpawnSignPattern(recognisedSign);
    }

    public void SpawnSignPattern(string sign)
    {
        prevRecognisedSign = recognisedSign;
        checkPointReached = startForCheckPoints;
        switch (sign)
        {
            case "a":
                if (!åPattern)
                {
                    recognisedMovingSign = "å"; 
                    DestroyAllPatterns();
                    åPattern = Instantiate(signPatterns[0], gameObject.transform.position, Quaternion.identity);
                    patternChildCount = signPatterns[0].transform.childCount;
                }
                break;
            case "i":
                if (!jPattern)
                {
                    recognisedMovingSign = "j";
                    DestroyAllPatterns();
                    jPattern = Instantiate(signPatterns[1], gameObject.transform.position, Quaternion.identity);
                    patternChildCount = signPatterns[1].transform.childCount;
                }
                break;
            case "o":
                if (!øPattern)
                {
                    recognisedMovingSign = "ø";
                    DestroyAllPatterns();
                    øPattern = Instantiate(signPatterns[2], gameObject.transform.position, Quaternion.identity);
                    patternChildCount = signPatterns[2].transform.childCount;
                }
                break;
            case "x":
                if (!zPattern)
                {
                    recognisedMovingSign = "z";
                    DestroyAllPatterns();
                    zPattern = Instantiate(signPatterns[3], gameObject.transform.position, Quaternion.identity);
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
            ClearPatternCheckpoint();
        }
        if (other.gameObject.name == (checkPointReached + 1).ToString())
        {
            MovingSignFailed();
        }
        //please dont look, im sorry
        if (other.gameObject.name == (checkPointReached + 2).ToString())
        {
            MovingSignFailed();
        }
        //please dont look, im sorry
        if (other.gameObject.name == (checkPointReached + 3).ToString())
        {
            MovingSignFailed();
        }
    }

    void ClearPatternCheckpoint()
    {
        Destroy(GameObject.Find(checkPointReached.ToString()));
        if (checkPointReached == patternChildCount)
        {
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
