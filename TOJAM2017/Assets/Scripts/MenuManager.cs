using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject credits;

	public void Quit()
    {
        Application.Quit();
    }

    public void Resize()
    {
        credits.transform.localScale = new Vector3(1, 1, 1);
    }
}

