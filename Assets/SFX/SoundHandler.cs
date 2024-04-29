using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System;

public class SoundHandler : MonoBehaviour {
    public static SoundHandler Instance;
    [SerializeField] AudioMixer effectMixer;
    [SerializeField]string masterVolumeName;
    [SerializeField] string musicVolumeName;
    [SerializeField] TMP_Text masterText;
    [SerializeField] TMP_Text musicText;
    [SerializeField] int audioChange;
    void Start(){
        Instance = this;
        SoundLevel();
    }

    void SoundLevel() {
        effectMixer.SetFloat(masterVolumeName, Mathf.Log10(0.001f+(float)Int32.Parse(masterText.text)/100f)*20);
        effectMixer.SetFloat(musicVolumeName, Mathf.Log10(0.001f+(float)Int32.Parse(musicText.text)/100f)*20);
    }

    public void ChangeText(string change){
        int musicVolume = Int32.Parse(musicText.text);
        int masterVolume = Int32.Parse(masterText.text);
        switch(change){
            case "master+":
                masterVolume += audioChange;
                break;
            case "master-":
                masterVolume -= audioChange;
                break;
            case "music+":
                musicVolume += audioChange;
                break;
            case "music-":
                musicVolume -= audioChange;
                break;
        }
        masterVolume = Mathf.Clamp(masterVolume, 0, 100);
        musicVolume = Mathf.Clamp(musicVolume, 0, 100);
        masterText.text = masterVolume.ToString();
        musicText.text = musicVolume.ToString();
        SoundLevel();
    }

}
