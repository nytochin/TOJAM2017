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
    public GameObject EndGamePanel;
    public ControllerManager controllerManager;
    public GameObject currentQuestion;
    public int[] choicePlayers;
    public int currentQuestionIndex;
    public bool answeringState = false;
    public GameObject image;

    private const string STARTGAME = "StartGame";
    private const string ENDGAME = "EndGame";
    private const string TEXT = "text";
    private const string AUDIO = "audio";
    private const string IMAGE = "image";
    private IEnumerator coroutine;

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
        // Initialize UI
        SetUIState(STARTGAME);
        // Start Game
        StartQuiz();
    }

    public void StartQuiz()
    {
        answeringState = false;
        // Initialize Question Manager
        questionManager.InitializeQuestions();
        currentQuestionIndex = -1;
        // Initialize Controller Manager
        controllerManager.SearchForControllers();
        // Initialize choicePlayers array
        choicePlayers = new int[controllerManager.controllersNumber.Length];
        ResetChoicePlayers();
        questionManager.InitializeAnswerArray(numberGameQuestions, controllerManager.controllersNumber.Length);
        // Display the first question
        DisplayNextQuestion();
    }

    public void SetUIState(string state)
    {
        if (state == STARTGAME)
        {
            // Activate question UI and answers UI
            questionUI.SetActive(true);
            answersUI.SetActive(true);
            // Activate score UI
            scoreUI.SetActive(false);
            EndGamePanel.SetActive(false);
        } else if (state == ENDGAME)
        {
            if (numberPlayers == 2)
            {
                // Cheat
                currentQuestionIndex--;
                // Deactivate question UI and answers UI
                questionUI.SetActive(false);
                answersUI.SetActive(false);
                // Activate score UI
                scoreUI.SetActive(true);
                scoreUI.GetComponentInChildren<Text>().text = "Score : " + questionManager.scores[0]+ "%"; // Two players
                                                                                                                       // Activate end game panel
                EndGamePanel.SetActive(true);
                GameObject endGameText = EndGamePanel.transform.FindChild("EndGameText").gameObject;
                endGameText.GetComponent<Text>().text = questionManager.GetEndGameTextByScore();
            } else if (numberPlayers == 4)
            {

            }
        }
    }

    public void DisplayNextQuestion()
    {


        currentQuestionIndex++; // for the first iteration, goes from -1 to 0
        if (currentQuestionIndex < numberGameQuestions)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject child = answersUI.transform.GetChild(i).gameObject;
                child.SetActive(false);
            }
            currentQuestion = questionManager.GetRandomQuestion();
            
            QuestionInfo qInfo = currentQuestion.GetComponent<QuestionInfo>();
            if (currentQuestion.GetComponent<QuestionInfo>().type == TEXT)
            {
                // Text type question
                questionUI.GetComponentInChildren<Text>().text = qInfo.question;

                for (int i = 0; i < qInfo.answers.Length; i++)
                {
                    GameObject child = answersUI.transform.GetChild(i).gameObject;
                    child.SetActive(true);
                    child.GetComponent<Text>().text = qInfo.answers[i];
                }
                // Text all displayed : the player can now answer
                answeringState = true;
            } else if (currentQuestion.GetComponent<QuestionInfo>().type == AUDIO)
            {
                // Audio type question
                questionUI.GetComponentInChildren<Text>().text = qInfo.question;
                AudioSource audio = qInfo.GetComponent<AudioSource>();
                questionManager.GetComponent<AudioSource>().clip = audio.clip;
                float timeAudio = questionManager.GetComponent<AudioSource>().clip.length + 2.0f;
                questionManager.GetComponent<AudioSource>().PlayDelayed(1); // Play with one second delay                
                coroutine = WaitForAudioToEnd(timeAudio, qInfo);
                StartCoroutine(coroutine);
            } else if (currentQuestion.GetComponent<QuestionInfo>().type == IMAGE)
            {
                // Image type question
                questionUI.GetComponentInChildren<Text>().text = qInfo.question;
                image.GetComponent<SpriteRenderer>().sprite = qInfo.GetComponent<SpriteRenderer>().sprite;

                for (int i = 0; i < qInfo.answers.Length; i++)
                {
                    GameObject child = answersUI.transform.GetChild(i).gameObject;
                    child.SetActive(true);
                    child.GetComponent<Text>().text = qInfo.answers[i];
                }
                // Text all displayed : the player can now answer
                answeringState = true;
            }
        } else
        {
            answeringState = false;
            Debug.Log("Game over");
            SetUIState(ENDGAME);            
        }
    }

    private IEnumerator WaitForAudioToEnd(float timeAudio, QuestionInfo qInfo)
    {
        yield return new WaitForSeconds(timeAudio);
        for (int i = 0; i < qInfo.answers.Length; i++)
        {
            GameObject child = answersUI.transform.GetChild(i).gameObject;
            child.SetActive(true);
            child.GetComponent<Text>().text = qInfo.answers[i];
        }
        // Text all displayed : the player can now answer
        answeringState = true;
    }

    public void Update()
    {
        if (AllChoicePlayersSelected() && answeringState)
        {
            answeringState = false;
            image.GetComponent<SpriteRenderer>().sprite = null;
            questionManager.SaveAnswers(currentQuestionIndex, choicePlayers);
            questionManager.UpdateScore();
            Debug.Log(questionManager.scores[0]);
            Debug.Log("Choices saved");
            // Animation - put the chairs back to normal
            // ...
            ResetChoicePlayers();            
            DisplayNextQuestion();
        }
    }


    private bool AllChoicePlayersSelected()
    {
        if (controllerManager.controllersNumber.Length != 0)
        {
            for (int i = 0; i < controllerManager.controllersNumber.Length; i++)
            {
                if (choicePlayers[i] == -1)
                {
                    return false;
                }
            }
            return true;
        } else
        {
            return false;
        }
    }

    public void ResetChoicePlayers()
    {
        for (int i = 0; i < controllerManager.controllersNumber.Length; i++)
        {
            choicePlayers[i] = -1;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
