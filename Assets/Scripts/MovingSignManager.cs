using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovingSignManager : MonoBehaviour
{
    [SerializeField] List<GameObject> signPatterns;

    //[SerializeField] TMP_Text currentSign;
    
    GameObject åPattern;
    GameObject jPattern;
    GameObject øPattern;
    GameObject zPattern;
    
    GameObject signPattern;

    float timeLimit = 5f;

    [SerializeField] Vector3 leftHandedRotation;

    TMP_Text recognisedSign;
    string prevRecognisedSign;

    string recognisedMovingSign;

    readonly int startForCheckPoints = 1;
    int checkPointReached;
    int patternChildCount;

    string handedness;
    

    Coroutine coroutineTimer;
    [SerializeField] GameObject head;

    void Start()
    {
        recognisedSign = GameObject.Find("SpecialSignText").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(recognisedSign == null) return;
        if(prevRecognisedSign == recognisedSign.text) return;
        DetectSign(recognisedSign.text);
    }

    float WrapHeadAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;
            
        return angle;
    }
    
    public void DetectSign(string sign)
    {
        handedness = ChooseHand.Instance.handedness;
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
            case "Z":
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
                var lRot = new Vector3(leftHandedRotation.x, leftHandedRotation.y + WrapHeadAngle(head.transform.eulerAngles.y), leftHandedRotation.z);
                signPattern = Instantiate(signPatterns[patternIndex], spawnpoint.transform.position, Quaternion.Euler(lRot));
                break;
            case "R":
                var rRot = new Vector3(0, WrapHeadAngle(head.transform.eulerAngles.y), 0);
                signPattern = Instantiate(signPatterns[patternIndex], spawnpoint.transform.position, Quaternion.Euler(rRot));
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
        if (checkPointReached >= patternChildCount - 1) //The arrows are also children of the prefab. This regulates the child count.
        {
            recognisedSign.text = recognisedMovingSign;
            print($"{recognisedMovingSign}");
            DestroyPattern();
            ActivateableSigns.ReceiveSpecialSign(recognisedMovingSign);
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
