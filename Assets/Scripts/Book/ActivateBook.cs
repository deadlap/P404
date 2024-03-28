using UnityEngine;

public class ActivateBook : MonoBehaviour
{
    GameObject nonDominantHand;
    [SerializeField] bool toggleFollow;
    Vector3 initialPos; 
    Quaternion initialRot;
    
    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
        nonDominantHand = GameObject.FindGameObjectWithTag("NonDominantHand");
    }

    void Update()
    {
        if (toggleFollow)
        {
            FollowHand();
        }

        if (!toggleFollow)
        {
            IdleState();
        }
    }

    void FollowHand()
    {
        transform.position = nonDominantHand.transform.position;
        transform.rotation = nonDominantHand.transform.rotation;
    }

    void IdleState()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;
    }
}
