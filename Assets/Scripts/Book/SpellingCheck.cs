using System;
using UnityEngine;
using TMPro;

public class SpellingCheck : MonoBehaviour { //ændre dens navn?

    [SerializeField] GameObject gestures;
    string currentWord;
    int letterIndex;

    [SerializeField] TMP_Text spellFloatingText;
    [SerializeField] TMP_Text playerProgress;

    void OnEnable() {
        BookEvents.SpellChosen += SetWord;
    }

    void OnDisable() {
        BookEvents.SpellChosen -= SetWord;
    }
    
    // void Update() {
    //     if (currentWord.Length == 0) {
    //         letterIndex = 0;
    //     }
    // }


    public void SignedLetterInput(string sign) {
        //char letter = char.Parse(sign);
        print("johnny bravo er et firben" + spellFloatingText.text);
        if (sign == currentWord[letterIndex].ToString()){
            letterIndex++;
            //kode der genererer det næste sign og giver positiv feedback ting
            playerProgress.text = sign;
            if (letterIndex > currentWord.Length) {
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

    bool CheckIncomingLetter(string letter, string word) { //input fra hvilket bogstav brugeren tegner
        print(letterIndex);
        print("letter: " + letter);
        print("word: " + word);
        
        
        return false;
    }

    void SetWord(string newWord){
        currentWord = newWord;
        letterIndex = 0;
        spellFloatingText.text = newWord;
        
        print("DR EGGMAN LEVER " + currentWord);
                
        if (newWord.Length > 0) {
            GenerateHandModel(newWord[0]);
            gestures.SetActive(true);
        }
        else {
            gestures.SetActive(false);
        }
    }

    public void GenerateHandModel(char letter) {
        
    }

}
