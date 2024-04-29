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
            GameObject.Find("Non-Dominant Hand Book Slot(Clone)").GetComponentInParent<SkinnedMeshRenderer>().enabled =
                false;
            BookEvents.OnSpellChosen(elementType);
            onCooldown = true;
            Invoke(nameof(ResetCooldown), cooldownTime);
        }
    }
}
