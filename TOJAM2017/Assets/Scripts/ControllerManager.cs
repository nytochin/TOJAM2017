using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {

    public int[] controllersNumber;

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
        if (GameManager.GM.currentQuestion)
        {
            // Wait for input of player 1
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
    }
}
