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

    GameObject spellCreationPosition;
    
    [SerializeField] Image chargeAnimation;
    [SerializeField] float choosingDuration;
    
    bool isChoosingHand;
    bool hasChosenHand;

    float fillThreshold = 0.0001f;
    [SerializeField] Vector3 leftPos;
    [SerializeField] Vector3 rightPos;

    GameObject rightHandMesh;
    GameObject leftHandMesh;
    
    [SerializeField] GameObject startPortal;
    AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        rightHandMesh = GameObject.Find("R_Hand");
        leftHandMesh = GameObject.Find("L_Hand");
    }


    void Update()
    {
        if (isChoosingHand)
        {
            chargeAnimation.fillAmount += Time.deltaTime / choosingDuration;
            audioSource.pitch = chargeAnimation.fillAmount + 1;
            if (Mathf.Abs(chargeAnimation.fillAmount - 1) < fillThreshold)
            {
                hasChosenHand = true;
                print("ding done");
            }
        }
        else
        {
            chargeAnimation.fillAmount -= Time.deltaTime * choosingDuration;
            audioSource.pitch = chargeAnimation.fillAmount + 1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "L_Palm" && other.gameObject.name != "R_Palm") return;
        isChoosingHand = true;
        hasChosenHand = false;
        spellCreationPosition = GameObject.Find("SpellCreationPoint");
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
        
        //SpawnTouchPointManager();
        
        //Sætter hvilken hånd spells bliver instantiated i
        SetSpellCreationPoint(dominantHandGO);

        nonDominantHand = GameObject.Find($"{nonDomHandString}_Palm");
        nonDominantHand.tag = "NonDominantHand";
        
        ShowHand(domHandString, nonDomHandString);
        SpawnBookPoint(nonDomHandString);
        if(startPortal)
            startPortal.SetActive(true);
        hasChosenHand = false;
    }

    void ShowHand(string domHandString, string nonDomHandString)
    {
        switch (domHandString)
        {
            case "R":
                rightHandMesh.SetActive(true);
                leftHandMesh.SetActive(false);
                break;
            case "L":
                rightHandMesh.SetActive(false);
                leftHandMesh.SetActive(true);
                break;
        }
    //        if(GameObject.Find($"{nonDomHandString}_Hand") == null) return;
        //GameObject.Find($"{nonDomHandString}_Hand").GetComponent<SkinnedMeshRenderer>().enabled = false;
        GameObject.Find($"{nonDomHandString}_IndexTip").tag = "Untagged";
        GameObject.Find($"{nonDomHandString}_MiddleTip").tag = "Untagged";
        //GameObject.Find($"{nonDomHandString}_RingTip").tag = "Untagged";
        //GameObject.Find($"{nonDomHandString}_LittleTip").tag = "Untagged";
        //GameObject.Find($"{nonDomHandString}_ThumbTip").tag = "Untagged";
        
      //      if(GameObject.Find($"{domHandString}_Hand") == null) return;
        //GameObject.Find($"{domHandString}_Hand").GetComponent<SkinnedMeshRenderer>().enabled = true;
        GameObject.Find($"{domHandString}_IndexTip").tag = "FingerTip";
        GameObject.Find($"{domHandString}_MiddleTip").tag = "FingerTip";
        //GameObject.Find($"{domHandString}_RingTip").tag = "FingerTip";
        //GameObject.Find($"{domHandString}_LittleTip").tag = "FingerTip";
        //GameObject.Find($"{domHandString}_ThumbTip").tag = "FingerTip";
    }

    void SetSpellCreationPoint(GameObject dominantHandGO)
    {
        var domHandPos = dominantHandGO.transform.position;
        spellCreationPosition.transform.SetParent(dominantHandGO.transform);
        switch (handedness)
        {
            case "L":
                spellCreationPosition.transform.localPosition = new Vector3(0, -0.1f, 0.04f);
                break;
            case "R":
                spellCreationPosition.transform.localPosition = new Vector3(0, -0.1f, 0.04f);
                break;
        }
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
            Destroy(GameObject.Find("Non-Dominant Hand Book Slot(Clone)"));
            bookFollowPoint = Instantiate(bookFollowPointPrefab, nonDominantHand.transform.position, nonDominantHand.transform.rotation * Quaternion.Euler(BookFollowPointPos(nonDomHand)), nonDominantHand.transform);
        }
        else
        {
            bookFollowPoint = Instantiate(bookFollowPointPrefab, nonDominantHand.transform.position, nonDominantHand.transform.rotation * Quaternion.Euler(BookFollowPointPos(nonDomHand)), nonDominantHand.transform);
        }
    }
    Vector3 BookFollowPointPos(string hand)
    {
        if (hand == "L_Palm")
            return leftPos;
        else
            return rightPos;
    }
}