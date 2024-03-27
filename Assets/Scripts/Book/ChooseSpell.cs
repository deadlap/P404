using System;
using UnityEngine;

public class ChooseSpell : MonoBehaviour
{
    public string elementType;
    bool hasChosen;

    [SerializeField] float cooldownTime = 1f;

    void Start()
    {
        elementType = gameObject.tag;
    }

    void Update()
    {
        if (!hasChosen) return;
        SelectionCooldown();
    }

    void SelectionCooldown()
    {
        var resetTime = cooldownTime;
        cooldownTime -= Time.deltaTime;
        if (cooldownTime < 0)
        {
            hasChosen = false;
            cooldownTime = resetTime;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DominantHand") && !hasChosen)
        {
            hasChosen = true;
            SpellChosenEvent.OnSpellChosen(elementType);
        }
    }
}
