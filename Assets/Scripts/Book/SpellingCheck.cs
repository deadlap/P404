using System;
using UnityEngine;
using TMPro;

public class SpellingCheck : MonoBehaviour {
    [SerializeField] GameObject spellCreationPosition;
    [SerializeField] GameObject gestures;
    string currentWord;
    int letterIndex;

    [SerializeField] GameObject handModelParent; //Where to spawn the spell
    
    [SerializeField] TMP_Text bookSpellText; //The spell the user is currently trying to cast
    [SerializeField] TMP_Text playerProgress; //The letters of the word the user has already performed

    [SerializeField] TMP_Text signedText; //The signed letter the user is currently performing 
    [SerializeField] TMP_Text currentSign; //The current sign the user needs to perform
    public static event Action DeleteSpellsEvent;
    public static void OnDeleteSpells() => DeleteSpellsEvent?.Invoke();
    string previousSign;

    AudioSource audioSource;
    [SerializeField] AudioClip[] terrackClips;
    [SerializeField] AudioClip[] ignisobClips;
    [SerializeField] AudioClip[] fulmentaClips;
    [SerializeField] AudioClip[] aquapyClips;
    [SerializeField] AudioClip[] ventushClips;
    void Start() {
        gestures.SetActive(true);
        signedText = GameObject.Find("SignText").GetComponent<TMP_Text>();
        spellCreationPosition = GameObject.Find("SpellCreationPoint");
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable() {
        BookEvents.SpellChosen += SetWord;
        DeleteSpellsEvent += DeleteSpells;
    }

    void OnDisable() {
        BookEvents.SpellChosen -= SetWord;
        DeleteSpellsEvent -= DeleteSpells;
    }

    void Update() {
        SignedLetterInput(signedText.text);
        previousSign = signedText.text;
    }

    public void SignedLetterInput(string _sign) {
        if(currentWord == null) return;
        if(signedText.text.Length == 0 || currentWord.Length == 0) return;
        if(signedText.text == previousSign) return;

        playerProgress.fontSize = bookSpellText.fontSize;
        _sign = _sign.ToUpper();
        
        string sign = _sign;

        if (_sign == "M" && currentWord[letterIndex].ToString().ToUpper() == "N") {
            sign = "N";
        }
        if (_sign == "U" && currentWord[letterIndex].ToString().ToUpper() == "R") {
            sign = "R";
        }

        if (sign == currentWord[letterIndex].ToString().ToUpper()){
            AudioFeedback(currentWord);
            letterIndex++;
            //kode der genererer det næste sign og giver positiv feedback ting
            if (letterIndex >= currentWord.Length) {
                GenerateSpell();
                SetWord("");
                DeleteHandModels();
            } else {
                playerProgress.text += currentWord[letterIndex-1].ToString().ToUpper();
                currentSign.text = currentWord[letterIndex].ToString().ToUpper();
                DeleteHandModels();
                GenerateHandModel(currentWord[letterIndex].ToString());
            }
        } else {
            //Kode der giver error besked fordi brugeren har signed forkert bogstav
        }
        //hvad end du skal bruge mr nikolaj
    }

    public void SetWord(string newWord){
        DeleteHandModels();
        currentWord = newWord;
        letterIndex = 0;
        bookSpellText.text = newWord;
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
    
    void DeleteSpells(){
        foreach (Transform child in spellCreationPosition.transform) {
	        GameObject.Destroy(child.gameObject);
        }
    }

    void GenerateSpell(){
        DeleteSpells();
        GameObject gameobject = Instantiate(Resources.Load("Spells/" + currentWord), spellCreationPosition.transform, false) as GameObject;
    }

    void AudioFeedback(string word)
    {
        switch (word.ToLower())
        {
            case "terrack":
                audioSource.PlayOneShot(terrackClips[letterIndex]);
                break;
            case "ignisob":
                audioSource.PlayOneShot(ignisobClips[letterIndex]);
                break;
            case "fulmenta":
                audioSource.PlayOneShot(fulmentaClips[letterIndex]);
                break;
            case "aquapy":
                audioSource.PlayOneShot(aquapyClips[letterIndex]);
                break;
            case "ventush":
                audioSource.PlayOneShot(ventushClips[letterIndex]);
                break;
        }
    }
    
    
}