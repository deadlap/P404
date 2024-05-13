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
    [SerializeField] GameObject bookPrefab;
    GameObject bookFollowPoint;
    GameObject book;

    public string handedness;

    GameObject spellCreationPosition;
    
    [SerializeField] Image chargeAnimation;
    [SerializeField] float choosingDuration;
    
    bool isChoosingHand;
    bool hasChosenHand;
    bool doneChoosing;

    float fillThreshold = 0.001f;
    [SerializeField] Vector3 leftPos;
    [SerializeField] Vector3 rightPos;

    GameObject rightHandMesh;
    GameObject leftHandMesh;
    
    [SerializeField] GameObject startPortal;
    AudioSource audioSource;

    public float handednessScale;
    
    void Awake()
    {
        handednessScale = 1.0f;
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
                if(GameObject.Find("Hand Gesture Detection") != null)
                    GameObject.Find("Hand Gesture Detection").GetComponent<XRHandTrackingEvents>().handedness = Handedness.Left;
                break;
            case "R_Palm":
                HandChosen(other.gameObject, "R", "L");
                if(GameObject.Find("Hand Gesture Detection") != null)
                    GameObject.Find("Hand Gesture Detection").GetComponent<XRHandTrackingEvents>().handedness = Handedness.Right;
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name is "L_Palm" or "R_Palm")
        {
            isChoosingHand = false;
            hasChosenHand = false;
            doneChoosing = false;
        }
    }

    void HandChosen(GameObject dominantHandGO, string domHandString, string nonDomHandString)
    {
        if(!hasChosenHand) return;
        if(doneChoosing) return;

        handedness = domHandString;
        dominantHandGO.gameObject.tag = "DominantHand";
        dominantHand = dominantHandGO;
        nonDominantHand = GameObject.Find($"{nonDomHandString}_Palm");
        nonDominantHand.tag = "NonDominantHand";
        
        //SpawnTouchPointManager();
        switch (domHandString) {
            case "R":
                handednessScale = 1.0f;
                break;
            case "L":
                handednessScale = -1.0f;
                break;
        }


        //Sætter hvilken hånd spells bliver instantiated i
        SetSpellCreationPoint(dominantHandGO);
        
        ShowHand(domHandString, nonDomHandString);
        
        var delay = 0.1f; //small delay to make sure other functions are loaded completely before calling
        Invoke(nameof(SpawnBookGraphic), delay);
        if(startPortal)
            startPortal.SetActive(true);
        doneChoosing = true;
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
        GameObject.Find($"{nonDomHandString}_IndexTip").tag = "Untagged";
        GameObject.Find($"{nonDomHandString}_MiddleTip").tag = "Untagged";
        
        GameObject.Find($"{domHandString}_IndexTip").tag = "FingerTip";
        GameObject.Find($"{domHandString}_MiddleTip").tag = "FingerTip";
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

    void SpawnBookGraphic()
    {
        if (bookFollowPoint)
        {
            //BookPageHandler.Instance.DestroyBook();
            Destroy(GameObject.Find("Book(Clone)"));
            Destroy(GameObject.Find("Non-Dominant Hand Book Slot(Clone)"));
        }
        book = Instantiate(bookPrefab, dominantHand.transform.position, dominantHand.transform.rotation);
        Invoke(nameof(SpawnBookPoint), 1f);
    }

    void SpawnBookPoint()
    {
        bookFollowPoint = Instantiate(bookFollowPointPrefab, nonDominantHand.transform.position, nonDominantHand.transform.rotation * Quaternion.Euler(BookFollowPointPos()), nonDominantHand.transform);
    }
    Vector3 BookFollowPointPos()
    {
        if (handedness == "L")
        {
            return rightPos;
        }
        return leftPos;
    }
}