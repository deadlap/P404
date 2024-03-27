using UnityEngine;

public class ActivateBook : MonoBehaviour
{
    GameObject nonDominantHand;
    [SerializeField] bool toggleFollow;
    
    void Start()
    {
        nonDominantHand = GameObject.FindGameObjectWithTag("NonDominantHand");
    }

    void Update()
    {
        if (toggleFollow)
        {
            transform.position = nonDominantHand.transform.position;
        }
    }
    
    
}
