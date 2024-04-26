using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] AudioMixer effectMixer;

    string volumeName = "MasterVolume";
    void SoundLevel(AudioMixer mixer, float newVolume)
    {
        mixer.SetFloat(volumeName, newVolume);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
