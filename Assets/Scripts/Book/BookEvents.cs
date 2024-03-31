using System;

public class BookEvents
{
    public static event Action<string> SpellChosen;
    public static event Action PrevPage;
    public static event Action NextPage;
    public static event Action PageTurning;
    
    public static void OnSpellChosen(string element) => SpellChosen?.Invoke(element);
    public static void OnPrevPage() => PrevPage?.Invoke();
    public static void OnNextPage() => NextPage?.Invoke();
    public static void OnPageTurning() => PageTurning?.Invoke();
    
    

}
