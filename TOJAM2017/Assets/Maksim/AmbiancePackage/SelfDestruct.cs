using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {
    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<AudioSource>().isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
