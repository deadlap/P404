using UnityEngine;

public class NextPage : MonoBehaviour
{
    [SerializeField] float cooldownTime;
    bool onCooldown;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DominantHand") && !onCooldown)
        {
            BookEvents.OnNextPage();
            onCooldown = true;
            Invoke(nameof(ResetCooldown), cooldownTime);
        }
    }
    
    void ResetCooldown()
    {
        onCooldown = false;
    }
}
