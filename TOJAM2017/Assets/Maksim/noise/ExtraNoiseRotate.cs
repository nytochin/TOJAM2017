using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraNoiseRotate : MonoBehaviour {
    public float intensity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, intensity, 0);
	}
}
