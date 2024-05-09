using TMPro;
using UnityEngine;

public class PlayerHint : MonoBehaviour
{
    [SerializeField] string[] hints;
    [SerializeField] TMP_Text hintText;
    int count;
    
    public void GiveHint()
    {
        hintText.text = hints[count];
        count++;
    }
}
