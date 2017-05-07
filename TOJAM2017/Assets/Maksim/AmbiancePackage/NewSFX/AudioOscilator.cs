using UnityEngine;
using System.Collections;

public class AudioOscilator : MonoBehaviour {

    public float oscilWidth;
    public float oscilStrength;
    private float oscilPos;
    private bool goingUp;
    private float currentIntensity;
    private AudioSource currentAudioSource;
    public AudioIntensity audIn;
    // Use this for initialization
    void Start () {
        goingUp = true;
        oscilPos = 0;

        
    }
	
	// Update is called once per frame
	void Update () {
        currentIntensity = audIn.intensity / 4f;
        currentAudioSource = gameObject.GetComponent<AudioSource>();
   
        if (currentAudioSource.pitch < 1f + 1f * currentIntensity * oscilWidth && goingUp) {
 
            currentAudioSource.pitch += oscilStrength * 0.1f * currentIntensity;
        }
        else
        {
            goingUp = false;
        }

        if (currentAudioSource.pitch > 1f - 1f * currentIntensity * oscilWidth && !goingUp)
        {
            currentAudioSource.pitch -= oscilStrength * 0.1f * currentIntensity;
        }
        else
        {
            goingUp = true;
        }
        currentAudioSource.volume = 1f * audIn.intensity * audIn.volIntensity * 0.0001f;
       
    }
}
