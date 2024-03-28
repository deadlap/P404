using UnityEngine;

public class ActivateBook : MonoBehaviour
{
    [SerializeField] bool toggleFollow;
    
    GameObject nonDominantHand;
    Vector3 initialPos; 
    Quaternion initialRot;
    
    void Start()
    {
        var tf = transform;
        initialPos = tf.position;
        initialRot = tf.rotation;
        nonDominantHand = GameObject.Find("Non-Dominant Hand Book Slot");
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
        var tf = transform;
        tf.position = nonDominantHand.transform.position;
        tf.rotation = nonDominantHand.transform.rotation;
    }

    void IdleState()
    {
        var tf = transform;
        tf.position = initialPos;
        tf.rotation = initialRot;
    }
}
