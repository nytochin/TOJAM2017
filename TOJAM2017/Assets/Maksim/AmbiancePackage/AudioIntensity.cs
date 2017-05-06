using UnityEngine;
using System.Collections;

public class AudioIntensity : MonoBehaviour 
{
    public float intensity;

	void Update()
	{
		intensity += Time.deltaTime * 0.1f;
	}
}
