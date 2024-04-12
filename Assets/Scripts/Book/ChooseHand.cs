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
                HandChosen(other.gameObject, "L", "R");
                GameObject.Find("Hand Gesture Detection").GetComponent<XRHandTrackingEvents>().handedness = Handedness.Left;
                break;
            case "R_Palm":
                HandChosen(other.gameObject, "R", "L");
                GameObject.Find("Hand Gesture Detection").GetComponent<XRHandTrackingEvents>().handedness = Handedness.Right;
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name is "L_Palm" or "R_Palm")
        {
            isChoosingHand = false;
        }
    }

    void HandChosen(GameObject dominantHandGO, string domHandString, string nonDomHandString)
    {
        if(!hasChosenHand) return;
        
        handedness = domHandString;
        dominantHandGO.gameObject.tag = "DominantHand";
        dominantHand = dominantHandGO;
        SpawnTouchPointManager();
        
        nonDominantHand = GameObject.Find($"{nonDomHandString}_Palm");
        nonDominantHand.tag = "NonDominantHand";
        
        ShowHand(domHandString, nonDomHandString);
        SpawnBookPoint(nonDomHandString);
    }

    void ShowHand(string domHandString, string nonDomHandString)
    {
        GameObject.Find($"{nonDomHandString}_IndexMetacarpal").tag = "Untagged";
        GameObject.Find($"{nonDomHandString}_MiddleMetacarpal").tag = "Untagged";
        GameObject.Find($"{nonDomHandString}_RingMetacarpal").tag = "Untagged";
        GameObject.Find($"{nonDomHandString}_LittleMetacarpal").tag = "Untagged";
        GameObject.Find($"{nonDomHandString}_ThumbMetacarpal").tag = "Untagged";
        GameObject.Find($"{nonDomHandString}_Hand").GetComponent<SkinnedMeshRenderer>().enabled = false;
        
        GameObject.Find($"{nonDomHandString}_IndexMetacarpal").tag = "FingerTip";
        GameObject.Find($"{nonDomHandString}_MiddleMetacarpal").tag = "FingerTip";
        GameObject.Find($"{nonDomHandString}_RingMetacarpal").tag = "FingerTip";
        GameObject.Find($"{nonDomHandString}_LittleMetacarpal").tag = "FingerTip";
        GameObject.Find($"{nonDomHandString}_ThumbMetacarpal").tag = "FingerTip";
        GameObject.Find($"{domHandString}_Hand").GetComponent<SkinnedMeshRenderer>().enabled = true;
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
    void SpawnBookPoint(string nonDomHand)
    {
        
        if (bookFollowPoint)
        {
            Destroy(bookFollowPoint);
            bookFollowPoint = Instantiate(bookFollowPointPrefab, nonDominantHand.transform.position, nonDominantHand.transform.rotation * Quaternion.Euler(BookFollowPointPos(nonDomHand)), nonDominantHand.transform);
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