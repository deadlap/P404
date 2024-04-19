using System;
using UnityEngine;
using TMPro;
using ElementNameSpace;

public class SpellingCheck : MonoBehaviour { //ændre dens navn?
    [SerializeField] GameObject spellCreationPosition;
    [SerializeField] GameObject gestures;
    string currentWord;
    int letterIndex;

    [SerializeField] GameObject handModelParent;
    [SerializeField] TMP_Text spellFloatingText;
    [SerializeField] TMP_Text playerProgress;

    void OnEnable() {
        BookEvents.SpellChosen += SetWord;
    }

    void OnDisable() {
        BookEvents.SpellChosen -= SetWord;
    }

    public void SignedLetterInput(string sign) {
        // Temporary testing ting
        // print("letterindex: "+ letterIndex);
        // print("Det den tror er ordet: " + spellFloatingText.text);
        // print("sign: " + sign);
        // print("current letter: "+ currentWord[letterIndex].ToString());
        // print("virker det?:" + (sign == currentWord[letterIndex].ToString().ToUpper()));
        
        if (sign == currentWord[letterIndex].ToString().ToUpper()){
            letterIndex++;
            //kode der genererer det næste sign og giver positiv feedback ting
            if (letterIndex >= currentWord.Length) {
                GenerateSpell();
                SetWord("");
                // Skal enables når alle hand models virker
                // DeleteHandModels();
            } else {
                playerProgress.text = currentWord[letterIndex].ToString().ToUpper();
                
                // Skal enables når alle hand models virker
                // GenerateHandModel(currentWord[letterIndex].ToString());
            }
        } else {
            //Kode der giver error besked fordi brugeren har signed forkert bogstav
        }
        //hvad end du skal bruge mr nikolaj
    }

    public void SetWord(string newWord){
        currentWord = newWord;
        letterIndex = 0;
        spellFloatingText.text = newWord;
        if (newWord.Length > 0) {
            // Skal enables når alle hand models virker
            // GenerateHandModel(newWord[0].ToString());
            playerProgress.text = newWord[0].ToString().ToUpper();
            gestures.SetActive(true);
        } else {
            gestures.SetActive(false);
            playerProgress.text = "";
        }
    }

    public void GenerateHandModel(string letter) {
        GameObject gameobject = Instantiate(Resources.Load("Hands/"+letter.ToUpper()), handModelParent.transform) as GameObject;
    }

    void DeleteHandModels(){
        foreach (Transform child in handModelParent.transform) {
	        GameObject.Destroy(child.gameObject);
        }
    }

    void GenerateSpell(){
        GameObject gameobject = Instantiate(Resources.Load("Spells/" + currentWord), spellCreationPosition.transform) as GameObject;
    }
}