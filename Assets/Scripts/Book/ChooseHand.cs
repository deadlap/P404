using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseHand : MonoBehaviour
{
    [SerializeField] GameObject bookFollowPointPrefab;
    GameObject bookFollowPoint;

    [SerializeField] Image chargeAnimation;
    [SerializeField] float choosingDuration;
    
    bool isChoosingHand;
    bool hasChosenHand;
    
    [SerializeField] Vector3 leftPos;
    [SerializeField] Vector3 rightPos;

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
        if (other.gameObject.name != "LeftHand" && other.gameObject.name != "RightHand") return;
        isChoosingHand = true;
        hasChosenHand = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name != "LeftHand" && other.gameObject.name != "RightHand") return;
        switch (other.gameObject.name)
        {
            case "LeftHand":
                HandChosen(other.gameObject, "RightHand");
                break;
            case "RightHand":
                HandChosen(other.gameObject, "LeftHand");
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name is "LeftHand" or "RightHand")
        {
            print("FUCK");
            isChoosingHand = false;
        }
    }

    void HandChosen(GameObject dominantHand, string nonDomHandString)
    {
        if(!hasChosenHand) return;
        
        print($"{dominantHand} valgt");
        
        dominantHand.gameObject.tag = "DominantHand";
        GameObject nonDominantHand = GameObject.Find($"{nonDomHandString}"); 
        nonDominantHand.tag = "NonDominantHand";
        if (bookFollowPoint)
        {
            Destroy(bookFollowPoint);
            bookFollowPoint = Instantiate(bookFollowPointPrefab, nonDominantHand.transform.position, Quaternion.Euler(BookFollowPointPos(nonDomHandString)), nonDominantHand.transform);
        }
        else
        {
            bookFollowPoint = Instantiate(bookFollowPointPrefab, nonDominantHand.transform.position, Quaternion.Euler(BookFollowPointPos(nonDomHandString)), nonDominantHand.transform);
        }
    }

    Vector3 BookFollowPointPos(string hand)
    {
        switch (hand)
        {
            case "LeftHand":
                return leftPos;
            case "RightHand":
                return rightPos;
        }
        return new Vector3();
    }
}