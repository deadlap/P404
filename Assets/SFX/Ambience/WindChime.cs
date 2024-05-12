using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindChime : MonoBehaviour
{
    float time;
    float randomTime;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;

    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        randomTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= randomTime)
        {
            PlayRandomChime();
            randomTime = Random.Range(minTime, maxTime);
            time = 0;
        }
    }

    void PlayRandomChime()
    {
        var randomClip = Random.Range(0, audioClips.Length);
        audioSource.PlayOneShot(audioClips[randomClip]);
    }
}
