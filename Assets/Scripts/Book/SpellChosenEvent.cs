using System;

public class SpellChosenEvent
{
    public static event Action<string> SpellChosen;
    
    public static void OnSpellChosen(string element) => SpellChosen?.Invoke(element);

}
