using System;

public class BookEvents
{
    public static event Action<string> SpellChosen;
    public static event Action PrevPage;
    public static event Action NextPage;
    public static event Action PageTurning;
    
    public static void OnSpellChosen(string elementName) => SpellChosen?.Invoke(elementName);
    public static void OnPrevPage() => PrevPage?.Invoke();
    public static void OnNextPage() => NextPage?.Invoke();
    public static void OnPageTurning() => PageTurning?.Invoke();
    
    

}
