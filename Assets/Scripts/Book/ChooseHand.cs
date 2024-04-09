using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Hands;

public class ChooseHand : MonoBehaviour
{
    public static ChooseHand Instance;
    GameObject dominantHand; 
    [SerializeField] GameObject touchPointManagerPrefab; 
    GameObject touchPointManager;
    
    GameObject nonDominantHand;
    [SerializeField] GameObject bookFollowPointPrefab;
    GameObject bookFollowPoint;

    public string handedness;
    
    [SerializeField] Image chargeAnimation;
    [SerializeField] float choosingDuration;
    
    bool isChoosingHand;
    bool hasChosenHand;
    
    [SerializeField] Vector3 leftPos;
    [SerializeField] Vector3 rightPos;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (isChoosingHand)
        {
            chargeAnimation.fillAmount += Time.deltaTime / choosingDuration;
            if (Mathf.Abs(chargeAnimation.fillAmount - 1) < 0.0001f)
            {
                hasChosenHand = true;
                print("ding done");
            }
        }
        else
        {
            chargeAnimation.fillAmount -= Time.deltaTime * choosingDuration;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "L_Palm" && other.gameObject.name != "R_Palm") return;
        isChoosingHand = true;
        hasChosenHand = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name != "L_Palm" && other.gameObject.name != "R_Palm") return;
        switch (other.gameObject.name)
        {
            case "L_Palm":
                HandChosen(other.gameObject, "L","R_Palm");
                break;
            case "R_Palm":
                HandChosen(other.gameObject, "R", "L_Palm");
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name is "L_Palm" or "R_Palm")
        {
            print("du gik ud");
            isChoosingHand = false;
        }
    }

    void HandChosen(GameObject dominantHandGO, string handednessString, string nonDomHandString)
    {
        if(!hasChosenHand) return;
        
        print($"{dominantHandGO} valgt");
        handedness = handednessString;
        dominantHandGO.gameObject.tag = "DominantHand";
        dominantHand = dominantHandGO;
        dominantHand.GetComponent<BoxCollider>().enabled = true;
        SpawnTouchPointManager();
        
        nonDominantHand = GameObject.Find($"{nonDomHandString}");
        nonDominantHand.GetComponent<BoxCollider>().enabled = false;
        nonDominantHand.tag = "NonDominantHand";
        
        SpawnBook(nonDomHandString);
    }

    void SpawnTouchPointManager()
    {
        if (touchPointManager)
        {
            Destroy(touchPointManager);
            touchPointManager = Instantiate(touchPointManagerPrefab, dominantHand.transform.position, Quaternion.identity, dominantHand.transform);
        }
        else
        {
            touchPointManager = Instantiate(touchPointManagerPrefab, dominantHand.transform.position, Quaternion.identity, dominantHand.transform);
        }
    }
    void SpawnBook(string nonDomHand)
    {
        
        if (bookFollowPoint)
        {
            Destroy(bookFollowPoint);
            bookFollowPoint = Instantiate(bookFollowPointPrefab, nonDominantHand.transform.position + Vector3.right * .5f, nonDominantHand.transform.rotation * Quaternion.Euler(BookFollowPointPos(nonDomHand)), nonDominantHand.transform);
        }
        else
        {
            bookFollowPoint = Instantiate(bookFollowPointPrefab, nonDominantHand.transform.position, nonDominantHand.transform.rotation * Quaternion.Euler(BookFollowPointPos(nonDomHand)), nonDominantHand.transform);
        }
    }
    Vector3 BookFollowPointPos(string hand)
    {
        switch (hand)
        {
            case "L_Palm":
                return leftPos;
            case "R_Palm":
                return rightPos;
        }
        return new Vector3();
    }
}