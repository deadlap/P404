using System;
using UnityEngine;

public class ChooseSpell : MonoBehaviour
{
    string elementType;
    bool onCooldown;

    [SerializeField] float cooldownTime;

    void Start()
    {
        elementType = gameObject.tag;
    }

    void ResetCooldown()
    {
        onCooldown = false;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DominantHand") && !onCooldown)
        {
            BookEvents.OnSpellChosen(elementType);
            onCooldown = true;
            Invoke(nameof(ResetCooldown), cooldownTime);
        }
    }
}
