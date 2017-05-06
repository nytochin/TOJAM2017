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
    public GameObject scoreUI;
    public ControllerManager controllerManager;
    public GameObject currentQuestion;
    public int[] choicePlayers;

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
        // Initialize choicePlayers array
        choicePlayers = new int[controllerManager.controllersNumber.Length];
        ResetChoicePlayers();
        questionManager.InitializeAnswerArray(numberGameQuestions, controllerManager.controllersNumber.Length);
        // Display the first question
        DisplayNextQuestion();
    }

    public void DisplayNextQuestion()
    {
        if (currentQuestionIndex < numberGameQuestions)
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
        } else
        {
            Debug.Log("Game over");
            // Deactivate question panel and answers panel
            questionUI.SetActive(false);
            answersUI.SetActive(false);
            // Activate score panel
            scoreUI.SetActive(true);
            scoreUI.GetComponentInChildren<Text>().text = "Score : " + questionManager.GetScoreTwoPlayers() + "%";
        }
    }

    public void Update()
    {
        if (AllChoicePlayersSelected())
        {
            questionManager.SaveAnswers(currentQuestionIndex, choicePlayers);
            Debug.Log("Choices saved");
            // Animation - put the chairs back to normal
            // ...
            ResetChoicePlayers();
            currentQuestionIndex++;
            DisplayNextQuestion();
        }
    }


    private bool AllChoicePlayersSelected()
    {
        for (int i = 0; i < controllerManager.controllersNumber.Length; i++)
        {
            if (choicePlayers[i] == -1)
            {
                return false;
            }
        }
        return true;
    }

    public void ResetChoicePlayers()
    {
        for (int i = 0; i < controllerManager.controllersNumber.Length; i++)
        {
            choicePlayers[i] = -1;
        }
    }
}
