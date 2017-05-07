using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatSound : MonoBehaviour {

    public AudioSource HBStart;
    public AudioSource HBEnd;

    public void Start()
    {
        HBStart = transform.Find("HBStart").GetComponent<AudioSource>();
        HBEnd = transform.Find("HBEnd").GetComponent<AudioSource>();
    }

    public void PlayHBStart()
    {
        HBStart.Play();
    }

    public void PlayHBEnd()
    {
        HBEnd.Play();
    }
}
