using UnityEngine;
using System.Collections;

public class ClipGenerator : MonoBehaviour {
    public AudioSource[] audSourArr;
    private float seconds;
    public float intensity;
    void Update()
    {
        seconds += Time.deltaTime;
        if (seconds > intensity)
        {
            Instantiate(audSourArr[UnityEngine.Random.Range(0, audSourArr.Length)], new Vector3(UnityEngine.Random.value * 20 - 10, UnityEngine.Random.value * 20 - 10, UnityEngine.Random.value * 20 - 10), Quaternion.identity);
            seconds = 0f;
        }
    }
}
