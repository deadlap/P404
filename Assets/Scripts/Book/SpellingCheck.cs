using UnityEngine;
using TMPro;

public class SpellingCheck : MonoBehaviour { //ændre dens navn?

    [SerializeField] string currentWord;
    [SerializeField] int letterIndex;

    [SerializeField] TMP_Text spellFloatingText;
    [SerializeField] TMP_Text playerProgress;

    void OnEnable() {
        BookEvents.SpellChosen += SetWord;
    }

    void OnDisable() {
        BookEvents.SpellChosen -= SetWord;
    }
    // Update is called once per frame
    void Update() {
        if (currentWord.Length == 0) {
            letterIndex = 0;
        }
    }


    public void SignedLetterInput(string sign) {
        
        if (CheckIncomingLetter(sign)) {
            playerProgress.text += sign;
            //kode der genererer det næste sign og giver positiv feedback ting
            if (letterIndex < currentWord.Length) {
                SetWord("");
                //Kode der caster spellen
            } else {
                GenerateHandModel(currentWord[letterIndex]);
            }
        } else {
            //Kode der giver error besked fordi brugeren har signed forkert bogstav
        }
        //hvad end du skal bruge mr nikolaj
    }

    bool CheckIncomingLetter(string letter) { //input fra hvilket bogstav brugeren tegner
        if (letter[0] == currentWord[letterIndex]){
            letterIndex++;
            return true;
        }
        return false;
    }

    void SetWord(string newWord){
        currentWord = newWord;
        letterIndex = 0;
        spellFloatingText.text = currentWord;
        
                
        if (newWord.Length > 0) {
            GenerateHandModel(newWord[0]);
        }
    }

    public void GenerateHandModel(char letter) {
        
    }

}
