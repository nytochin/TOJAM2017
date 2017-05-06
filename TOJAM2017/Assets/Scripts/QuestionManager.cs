using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour {

    public List<GameObject> questions;
    public float[] scores;

    private List<GameObject> questionsLeft;
    private int[][] answersPlayersPlayer1Guesses; // [number of questions][number of players]
    private int[][] answersPlayersPlayer2Guesses; // [number of questions][number of players]
    private const string ENDGAMETEXT50 = "Congratulations !";
    private const string ENDGAMETEXT00 = "Loser !";

    public void InitializeQuestions()
    {
        questionsLeft = new List<GameObject>(questions);
        scores = new float[2];
        scores[0] = 0.0f;
        scores[1] = 0.0f;
    }

    public void InitializeAnswerArray(int numberGameQuestions, int numberPlayers)
    {
        answersPlayersPlayer1Guesses = new int[numberGameQuestions][];
        answersPlayersPlayer2Guesses = new int[numberGameQuestions][];
        for (int i = 0; i < numberGameQuestions; i++)
        {
            int[] subArray1 = new int[numberPlayers];
            int[] subArray2 = new int[numberPlayers];
            for (int j = 0; j < subArray1.Length; j++)
            {
                subArray1[j] = -1;
                subArray2[j] = -1;
            }
            answersPlayersPlayer1Guesses[i] = subArray1;
            answersPlayersPlayer2Guesses[i] = subArray2;
        }
    }

    public GameObject GetRandomQuestion()
    {
        int numberQuestionsLeft = questionsLeft.Count;
        int questionSelectedIndex = UnityEngine.Random.Range(0, numberQuestionsLeft - 1);
        GameObject questionSelected = questionsLeft[questionSelectedIndex];
        questionsLeft.RemoveAt(questionSelectedIndex);
        return questionSelected;
    }

    public float[] GetScoreTwoPlayers() // return score of good guesses of player 1 and score or good guesses of player 2
    {
        int correctGuessesPlayer1 = 0;
        int correctGuessesPlayer2 = 0;
        float[] scores = new float[2];
        int numberQuestions = GameManager.GM.currentQuestionIndex + 1; // because currentQuestionIndex starts at 0
        for (int i = 0; i < numberQuestions; i++)
        {
            
            if (answersPlayersPlayer1Guesses[i][0] == answersPlayersPlayer1Guesses[i][1])
            {
                correctGuessesPlayer1++;
            }
            if (answersPlayersPlayer2Guesses[i][0] == answersPlayersPlayer2Guesses[i][1])
            {
                correctGuessesPlayer2++;
            }

        }
        scores[0] = (float)correctGuessesPlayer1 / numberQuestions * 100.0f;
        scores[1] = (float)correctGuessesPlayer2 / numberQuestions * 100.0f;
        return scores;
    }

    public float[] GetScoreFourPlayers()
    {
        float[] scores = new float[2];
        int correctGuessesPair1 = 0;
        int correctGuessesPair2 = 0;
        int numberQuestions = GameManager.GM.currentQuestionIndex + 1; // because currentQuestionIndex starts at 0
        for (int i = 0; i < numberQuestions; i++)
        {

            if (answersPlayersPlayer1Guesses[i][0] == answersPlayersPlayer1Guesses[i][1])
            {
                correctGuessesPair1++;
            }
            if (answersPlayersPlayer1Guesses[i][2] == answersPlayersPlayer1Guesses[i][3])
            {
                correctGuessesPair2++;
            }

        }
        scores[0] = correctGuessesPair1 / numberQuestions * 100.0f;
        scores[1] = correctGuessesPair2 / numberQuestions * 100.0f;
        return scores; 
    }

    public void SaveAnswers(int currentQuestionIndex, int[] choicePlayers, bool player1IsGuessing) {
        if (player1IsGuessing)
        {
            for (int i = 0; i < choicePlayers.Length; i++)
            {
                answersPlayersPlayer1Guesses[currentQuestionIndex][i] = choicePlayers[i];
            }
        } else
        {
            for (int i = 0; i < choicePlayers.Length; i++)
            {
                answersPlayersPlayer2Guesses[currentQuestionIndex][i] = choicePlayers[i];
            }
        }
    }

    public string GetEndGameTextByScore()
    {
        if (GameManager.GM.numberPlayers == 2)
        {
            if (scores[0] > 50.0f)
            {
                return ENDGAMETEXT50;
            }
            else
            {
                return ENDGAMETEXT00;
            }
        } else if (GameManager.GM.numberPlayers == 4)
        {
            return "Four players text not done yet";
        }
        return null;
    }

    public void UpdateScore()
    {
        if (GameManager.GM.numberPlayers == 2)
        {
            float[] sc = GetScoreTwoPlayers();
            scores[0] = sc[0];
            scores[1] = sc[1];
        } else if (GameManager.GM.numberPlayers == 4)
        {
            scores = GetScoreFourPlayers();
        }
    }
}
