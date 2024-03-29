using UnityEngine;

public class PreviousPage : MonoBehaviour
{
    [SerializeField] float cooldownTime;
    bool onCooldown;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DominantHand") && !onCooldown)
        {
            BookEvents.OnPrevPage();
            onCooldown = true;
            Invoke(nameof(ResetCooldown), cooldownTime);
        }
    }
    
    void ResetCooldown()
    {
        onCooldown = false;
    }
}
