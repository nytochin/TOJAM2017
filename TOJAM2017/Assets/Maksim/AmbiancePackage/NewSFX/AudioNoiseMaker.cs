using UnityEngine;
using System.Collections;
using System;
public class AudioNoiseMaker : MonoBehaviour {


    public float noiseWidth;
    public float noiseStrength;
    private bool goingUp;
    private float currentIntensity;
    private AudioSource currentAudioSource;
    private float noisePos;
    public AudioIntensity audIn;
    public float volumeIntensity;
    // Use this for initialization
    void Start()
    {
        goingUp = true;
        noisePos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentIntensity = audIn.intensity / 4f;
        currentAudioSource = gameObject.GetComponent<AudioSource>();
        noisePos += Time.deltaTime;
        if (noisePos > (1/ noiseWidth * 1/currentIntensity))
        {
            noisePos = 0;
            currentAudioSource.pitch = 1 + noiseStrength * 0.1f * currentIntensity * (UnityEngine.Random.value * 2 - 1);
        }

        currentAudioSource.volume = 1f * audIn.intensity * audIn.volIntensity * 0.0001f;
    }
}
