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
                HandChosen(other.gameObject, "R_Palm");
                break;
            case "R_Palm":
                HandChosen(other.gameObject, "L_Palm");
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name is "L_Palm" or "R_Palm")
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
            case "L_Palm":
                return leftPos;
            case "R_Palm":
                return rightPos;
        }
        return new Vector3();
    }
}