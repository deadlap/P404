using System;
using UnityEngine;
using TMPro;

public class SpellingCheck : MonoBehaviour { //ændre dens navn?

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
    
    // void Update() {
    //     if (currentWord.Length == 0) {
    //         letterIndex = 0;
    //     }
    // }

    public void SignedLetterInput(string sign) {
        // print("letterindex: "+ letterIndex);
        // print("Det den tror er ordet: " + spellFloatingText.text); 
        // print("sign: " + sign);
        // print("current letter: "+ currentWord[letterIndex].ToString());
        // print("virker det?:" + (sign == currentWord[letterIndex].ToString().ToUpper()));
        if (sign == currentWord[letterIndex].ToString().ToUpper()){
            letterIndex++;
            //kode der genererer det næste sign og giver positiv feedback ting
            if (letterIndex >= currentWord.Length) {
                SetWord("");
                DeleteHandModels();
            } else {
                playerProgress.text = currentWord[letterIndex].ToString().ToUpper();
                GenerateHandModel(currentWord[letterIndex].ToString());
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
        playerProgress.text = newWord[0].ToString().ToUpper();
        if (newWord.Length > 0) {
            GenerateHandModel(newWord[0].ToString());
            gestures.SetActive(true);
        }
        else {
            gestures.SetActive(false);
        }
    }

    public void GenerateHandModel(string letter) {
        GameObject handModel = Instantiate(Resources.Load("Hands/"+letter.ToUpper()), handModelParent.transform) as GameObject;
    }

    void DeleteHandModels(){
        foreach (Transform child in handModelParent.transform) {
	        GameObject.Destroy(child.gameObject);
        }
    }

}
