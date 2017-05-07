using UnityEngine;
using System.Collections;

public class RandomizePitch : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<AudioSource>().pitch = 1 + (UnityEngine.Random.value * 0.50f - 0.25f);
    }

}
