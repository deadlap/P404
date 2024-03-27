using TMPro;
using UnityEngine;

public class SpellToCast : MonoBehaviour
{
    [SerializeField] TMP_Text spellFloatingText;
    string spellElement;
    
    void OnEnable()
    {
        SpellChosenEvent.SpellChosen += ChosenSpell;
    }

    void OnDisable()
    {
        SpellChosenEvent.SpellChosen -= ChosenSpell;
    }

    void ChosenSpell(string element)
    {
        spellElement = element;
        spellFloatingText.text = spellElement;
        print($"Player has chosen {spellElement}");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
