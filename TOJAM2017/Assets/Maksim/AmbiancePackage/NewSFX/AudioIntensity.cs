using UnityEngine;
using System.Collections;

public class AudioIntensity : MonoBehaviour 
{
    public float intensity;
    public float volIntensity;
	void Update()
	{
		intensity += Time.deltaTime * 0.1f;
	}
}
