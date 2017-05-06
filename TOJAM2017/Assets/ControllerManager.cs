using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {

    public int[] controllersNumber;

    public int[] choicePlayers;

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
        // Initialize choicePlayers array
        choicePlayers = new int[controllersNumber.Length];
    }

    private void Update()
    {
        if (GameManager.GM.currentQuestion)
        {
            // Wait for input of player 1
            for (int i = 0; i < controllersNumber.Length; i++)
            {
                if (choicePlayers[i] == -1)
                {
                    if (GameManager.GM.currentQuestion.GetComponent<QuestionInfo>().answers.Length == 2)
                    {
                        if (Input.GetButtonDown("A" + (i + 1).ToString()))
                        {
                            Debug.Log("A" + (i + 1) + " button pressed");
                            choicePlayers[i] = 0;
                        }
                        else if (Input.GetButtonDown("B" + (i + 1).ToString()))
                        {
                            Debug.Log("B" + (i + 1) + " button pressed");
                            choicePlayers[i] = 1;
                        }
                    }
                    else if (GameManager.GM.currentQuestion.GetComponent<QuestionInfo>().answers.Length == 4)
                    {
                        if (Input.GetButtonDown("A" + (i + 1).ToString()))
                        {
                            Debug.Log("A" + (i + 1) + " button pressed");
                            choicePlayers[i] = 0;
                        }
                        else if (Input.GetButtonDown("B" + (i + 1).ToString()))
                        {
                            Debug.Log("B" + (i + 1) + " button pressed");
                            choicePlayers[i] = 1;
                        }
                        else if (Input.GetButtonDown("X" + (i + 1).ToString()))
                        {
                            Debug.Log("X" + (i + 1) + " button pressed");
                            choicePlayers[i] = 2;
                        }
                        else if (Input.GetButtonDown("Y" + (i + 1).ToString()))
                        {
                            Debug.Log("Y" + (i + 1) + " button pressed");
                            choicePlayers[i] = 3;
                        }
                    }
                }
            }
        }
    }

    public void ResetChoicePlayers()
    {
        for (int i = 0; i < controllersNumber.Length; i++)
        {
            choicePlayers[i] = -1;
        }
    }
}
