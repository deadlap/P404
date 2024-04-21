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
    [SerializeField] TMP_Text spellFloatingText; //Rename
    [SerializeField] TMP_Text playerProgress;
    [SerializeField] TMP_Text signedText; //
    
    [SerializeField] TMP_Text currentSign;

    void OnEnable() {
        BookEvents.SpellChosen += SetWord;
    }

    void OnDisable() {
        BookEvents.SpellChosen -= SetWord;
    }

    void Update()
    {
        SignedLetterInput(signedText.text);
    }

    public void SignedLetterInput(string _sign) {
        _sign = _sign.ToString().ToUpper();
        if(signedText.text.Length == 0 || currentWord.Length == 0) return;
        
        string sign = _sign;

        if (_sign == "M" && currentWord[letterIndex].ToString().ToUpper() == "N"){
            sign = "N";
        }
        if (_sign == "U" && currentWord[letterIndex].ToString().ToUpper() == "R"){
            sign = "R";
        }

        if (sign == currentWord[letterIndex].ToString().ToUpper()){
            letterIndex++;
            //kode der genererer det næste sign og giver positiv feedback ting
            if (letterIndex >= currentWord.Length) {
                GenerateSpell();
                SetWord("");
                // Skal enables når alle hand models virker
                // DeleteHandModels();
            } else {
                playerProgress.text += currentWord[letterIndex].ToString().ToUpper();
                currentSign.text = currentWord[letterIndex].ToString().ToUpper();
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
        playerProgress.text = "";
        currentSign.text = "";
        if (newWord.Length > 0) {
            // Skal enables når alle hand models virker
            GenerateHandModel(newWord[0].ToString());
            currentSign.text = currentWord[letterIndex].ToString().ToUpper();
            gestures.SetActive(true);
        } else {
            gestures.SetActive(false);
            playerProgress.text = "";
            currentSign.text = "";
        }
    }

    public void GenerateHandModel(string letter) {
        GameObject gameobject = Instantiate(Resources.Load("Models/Handshapes/"+letter.ToUpper() + "_Handshape"), handModelParent.transform) as GameObject;
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