using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager GM = null;

    public int numberPlayers;
    public QuestionManager questionManager;
    public int numberGameQuestions;
    public GameObject questionUI;
    public GameObject answersUI;
    public ControllerManager controllerManager;
    public GameObject currentQuestion;

    private int currentQuestionIndex;

    public void Awake()
    {
        if (GM == null)
        {
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        // Initialize Question Manager
        questionManager.InitializeQuestions();
        currentQuestionIndex = 0;
        // Initialize Controller Manager
        controllerManager.SearchForControllers();
        controllerManager.ResetChoicePlayers();
        questionManager.InitializeAnswerArray(numberGameQuestions, controllerManager.controllersNumber.Length);

        DisplayNextQuestion();
    }

    public void DisplayNextQuestion()
    {
        currentQuestion = questionManager.GetRandomQuestion();
        QuestionInfo qInfo = currentQuestion.GetComponent<QuestionInfo>();
        // Display the question and the answers in the UI
        questionUI.GetComponentInChildren<Text>().text = qInfo.question;
        for (int i = 0; i < qInfo.answers.Length; i++)
        {
            GameObject child = answersUI.transform.GetChild(i).gameObject;
            child.SetActive(true);
            child.GetComponent<Text>().text = qInfo.answers[i];
        }
    }

    public void Update()
    {
        if (AllChoicePlayersSelected())
        {
            questionManager.SaveAnswers(currentQuestionIndex, controllerManager.choicePlayers);
            Debug.Log("Choices saved");
            // Reset choices : TO DO
        }
    }


    private bool AllChoicePlayersSelected()
    {
        for (int i = 0; i < controllerManager.controllersNumber.Length; i++)
        {
            if (controllerManager.choicePlayers[i] == -1)
            {
                return false;
            }
        }
        return true;
    }
}
