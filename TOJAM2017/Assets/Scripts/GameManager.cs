using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public bool player1IsGuessing;
    public GameObject playAgain;
    public GameObject chairPlayer1;
    public GameObject chairPlayer2;
    public GameObject chairPlayerStatic1;
    public GameObject chairPlayerStatic2;

    private const string STARTGAME = "StartGame";
    private const string ENDGAME = "EndGame";
    private const string TEXT = "text";
    private const string AUDIO = "audio";
    private const string IMAGE = "image";
    private IEnumerator coroutine;
    private float SPEEDTEXT = 0.02f;
    private AudioSource audioTransition;
    private AudioSource lightOn;

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

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(2.0f);
        // Deactivate FadeOut
        GameObject.Find("FadeOut").SetActive(false);
        // Initialize UI
        SetUIState(STARTGAME);
        // Start Game
        StartQuiz();
    }

    public void StartQuiz()
    {
        // Initialize chairs state
        chairPlayer1.SetActive(true);
        chairPlayer2.SetActive(true);
        chairPlayerStatic1.SetActive(false);
        chairPlayerStatic2.SetActive(false);
        audioTransition = GetComponent<AudioSource>();
        player1IsGuessing = true;
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
                // EventSystem.current.SetSelectedGameObject(playAgain);
                StartCoroutine("HighlightBtn");

                // Cheat
                currentQuestionIndex--;
                // Deactivate question UI and answers UI
                questionUI.SetActive(false);
                answersUI.SetActive(false);
                // Activate score UI
                scoreUI.SetActive(true);
                scoreUI.GetComponentInChildren<Text>().text = "Player 1 Score : " + questionManager.scores[0]+ "% - " + "Player 2 Score : " + questionManager.scores[1] + "%"; // Two players score
                // Activate end game panel
                EndGamePanel.SetActive(true);
                GameObject endGameText = EndGamePanel.transform.FindChild("EndGameText").gameObject;
                endGameText.GetComponent<Text>().text = questionManager.GetEndGameTextByScore();
            } else if (numberPlayers == 4)
            {
                // TO DO
            }
        }
    }

    IEnumerator HighlightBtn()
    {
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        yield return null;
        es.SetSelectedGameObject(es.firstSelectedGameObject);
    }

    public void DisplayNextQuestion()
    {
        if (player1IsGuessing)
        {
            chairPlayer1.SetActive(true);
            chairPlayer2.SetActive(false);
            chairPlayerStatic1.SetActive(false);
            chairPlayerStatic2.SetActive(true);
            currentQuestionIndex++; // for the first iteration, goes from -1 to 0 - go to next question if turn of player 1 again
            currentQuestion = questionManager.GetRandomQuestion();

            if (currentQuestionIndex < numberGameQuestions)
            {
                // Reset question and answers and image
                for (int i = 0; i < 4; i++)
                {
                    GameObject child = answersUI.transform.GetChild(i).gameObject;
                    child.SetActive(false);
                }
                questionUI.GetComponentInChildren<Text>().text = "";
                image.GetComponent<SpriteRenderer>().sprite = null;

                QuestionInfo qInfo = currentQuestion.GetComponent<QuestionInfo>();
                if (currentQuestion.GetComponent<QuestionInfo>().type == TEXT)
                {
                    // Text type question
                    coroutine = Text(qInfo);
                    StartCoroutine(coroutine);
                    //questionUI.GetComponentInChildren<Text>().text = qInfo.question;
                }
                else if (currentQuestion.GetComponent<QuestionInfo>().type == AUDIO)
                {
                    // Audio type question                    
                    coroutine = Audio(qInfo);
                    StartCoroutine(coroutine);
                }
                else if (currentQuestion.GetComponent<QuestionInfo>().type == IMAGE)
                {
                    // Image type question
                    coroutine = Image(qInfo);
                    StartCoroutine(coroutine);
                }
            }
            else
            {
                answeringState = false;
                chairPlayer1.SetActive(false);
                chairPlayer2.SetActive(false);
                chairPlayerStatic1.SetActive(true);
                chairPlayerStatic2.SetActive(true);
                Debug.Log("Game over");
                SetUIState(ENDGAME);
            }
        }  else
        {
            answeringState = true;
            chairPlayer1.SetActive(false);
            chairPlayer2.SetActive(true);
            chairPlayerStatic1.SetActive(true);
            chairPlayerStatic2.SetActive(false);
        }    
    }

    private IEnumerator Image(QuestionInfo qInfo)
    {
        foreach (char letter in qInfo.question.ToCharArray())
        {
            questionUI.GetComponentInChildren<Text>().text += letter;

            yield return 0;
            yield return new WaitForSeconds(SPEEDTEXT);
        }
        image.GetComponent<SpriteRenderer>().sprite = qInfo.GetComponent<SpriteRenderer>().sprite;

        for (int i = 0; i < qInfo.answers.Length; i++)
        {
            GameObject child = answersUI.transform.GetChild(i).gameObject;
            child.SetActive(true);
            child.GetComponentInChildren<Text>().text = qInfo.answers[i];
        }
        // Text all displayed : the player can now answer
        answeringState = true;
    }

    private IEnumerator Text(QuestionInfo qInfo)
    {
        foreach (char letter in qInfo.question.ToCharArray())
{
            questionUI.GetComponentInChildren<Text>().text += letter;
            //if (typeSound1 && typeSound2)
            //{
            //    SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
            //}
            yield return 0;
            yield return new WaitForSeconds(SPEEDTEXT);
        }
        for (int i = 0; i < qInfo.answers.Length; i++)
        {
            GameObject child = answersUI.transform.GetChild(i).gameObject;
            child.SetActive(true);
            child.GetComponentInChildren<Text>().text = qInfo.answers[i];
        }
        // Text all displayed : the player can now answer
        answeringState = true;
    }

    private IEnumerator Audio(QuestionInfo qInfo)
    {
        // Audio type question
        foreach (char letter in qInfo.question.ToCharArray())
        {
            questionUI.GetComponentInChildren<Text>().text += letter;
            yield return 0;
            yield return new WaitForSeconds(SPEEDTEXT);
        }
        AudioSource audio = qInfo.GetComponent<AudioSource>();
        questionManager.GetComponent<AudioSource>().clip = audio.clip;
        float timeAudio = questionManager.GetComponent<AudioSource>().clip.length + 2.0f;
        questionManager.GetComponent<AudioSource>().PlayDelayed(1); // Play with one second delay  
        yield return new WaitForSeconds(timeAudio);
        for (int i = 0; i < qInfo.answers.Length; i++)
        {
            GameObject child = answersUI.transform.GetChild(i).gameObject;
            child.SetActive(true);
            child.GetComponentInChildren<Text>().text = qInfo.answers[i];
        }
        // Text all displayed : the player can now answer
        answeringState = true;
    }

    public void Update()
    {
        if (AllChoicePlayersSelected() && answeringState)
        {
            audioTransition.Play();
            audioTransition.SetScheduledEndTime(AudioSettings.dspTime+0.2f);
            answeringState = false;            
            questionManager.SaveAnswers(currentQuestionIndex, choicePlayers, player1IsGuessing);
            questionManager.UpdateScore();
            Debug.Log(questionManager.scores[0]);
            Debug.Log("Choices saved");
            // Animation - put the chairs back to normal
            // ...
            ResetChoicePlayers();
            player1IsGuessing = !player1IsGuessing;
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
