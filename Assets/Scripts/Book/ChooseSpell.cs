using System;
using UnityEngine;

public class ChooseSpell : MonoBehaviour
{
    [SerializeField] string elementType;
    bool onCooldown;
    
    [SerializeField] float cooldownTime;

    void ResetCooldown()
    {
        onCooldown = false;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FingerTip") && !onCooldown)
        {
            
            BookEvents.OnSpellChosen(elementType);
            onCooldown = true;
            Invoke(nameof(ResetCooldown), cooldownTime);
        }
    }
}
