using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class ControllerManager : MonoBehaviour {

    public int[] controllersNumber;

    private float remainingTime;

    public void SearchForControllers()
    {
        string[] controllers = Input.GetJoystickNames();
        List<int> connectedControllers = new List<int>();

        for (int i = 0; i < controllers.Length; i++)
        {
            if (controllers[i] != "")
            {
                connectedControllers.Add(i + 1);
            }
        }

        controllersNumber = connectedControllers.ToArray();

    }

    private void Update()
    {
        if (GameManager.GM.currentQuestion && GameManager.GM.answeringState)
        {
            // Wait for input of players
            for (int i = 0; i < controllersNumber.Length; i++)
            {
                if (GameManager.GM.choicePlayers[i] == -1)
                {
                    if (GameManager.GM.currentQuestion.GetComponent<QuestionInfo>().answers.Length == 2)
                    {
                        if (Input.GetButtonDown("A" + (i + 1).ToString()))
                        {
                            Debug.Log("A" + (i + 1) + " button pressed");
                            GameManager.GM.choicePlayers[i] = 0;
                            // Animation - Make chair fall down
                            // ...
                        }
                        else if (Input.GetButtonDown("B" + (i + 1).ToString()))
                        {
                            Debug.Log("B" + (i + 1) + " button pressed");
                            GameManager.GM.choicePlayers[i] = 1;
                            // Animation - Make chair fall down
                            // ...
                        }
                    }
                    else if (GameManager.GM.currentQuestion.GetComponent<QuestionInfo>().answers.Length == 4)
                    {
                        if (Input.GetButtonDown("A" + (i + 1).ToString()))
                        {
                            Debug.Log("A" + (i + 1) + " button pressed");
                            GameManager.GM.choicePlayers[i] = 0;
                            // Animation - Make chair fall down
                            // ...
                        }
                        else if (Input.GetButtonDown("B" + (i + 1).ToString()))
                        {
                            Debug.Log("B" + (i + 1) + " button pressed");
                            GameManager.GM.choicePlayers[i] = 1;
                            // Animation - Make chair fall down
                            // ...
                        }
                        else if (Input.GetButtonDown("X" + (i + 1).ToString()))
                        {
                            Debug.Log("X" + (i + 1) + " button pressed");
                            GameManager.GM.choicePlayers[i] = 2;
                            // Animation - Make chair fall down
                            // ...
                        }
                        else if (Input.GetButtonDown("Y" + (i + 1).ToString()))
                        {
                            Debug.Log("Y" + (i + 1) + " button pressed");
                            GameManager.GM.choicePlayers[i] = 3;
                            // Animation - Make chair fall down
                            // ...
                        }
                    }
                }
            }
        }
        if (GameManager.GM.answeringState && GameManager.GM.player1IsGuessing)
        {
            VibrateController(0, 1, 0.15f);
        } else if (GameManager.GM.answeringState && !GameManager.GM.player1IsGuessing)
        {
            VibrateController(1, 1, 0.15f);
        }
    }

    public void VibrateController(int controllerNumber, float duration, float intensity)
    {
        remainingTime = duration;
        StartCoroutine(vibrate(controllerNumber, intensity));
    }

    private IEnumerator vibrate(int controllerNumber, float intensity)
    {
        while (remainingTime > 0)
        {
            yield return 0;

            remainingTime -= Time.deltaTime;
            GamePad.SetVibration((PlayerIndex)controllerNumber, intensity, intensity);
        }
        GamePad.SetVibration((PlayerIndex)controllerNumber, 0, 0);
    }
}
