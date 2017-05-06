using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour {

    public List<GameObject> questions;
    public float[] scores;

    private List<GameObject> questionsLeft;
    private int[][] answersPlayers; // [number of questions][number of players]
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
        answersPlayers = new int[numberGameQuestions][];
        for (int i = 0; i < numberGameQuestions; i++)
        {
            int[] subArray = new int[numberPlayers];
            for (int j = 0; j < subArray.Length; j++)
            {
                subArray[j] = -1;
            }
            answersPlayers[i] = subArray;
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

    public float GetScoreTwoPlayers()
    {
        int correctGuesses = 0;
        int numberQuestions = GameManager.GM.currentQuestionIndex + 1; // because currentQuestionIndex starts at 0
        for (int i = 0; i < numberQuestions; i++)
        {
            
            if (answersPlayers[i][0] == answersPlayers[i][1])
            {
                correctGuesses++;
            }
            
        }
        return (float)correctGuesses / numberQuestions * 100.0f;
    }

    public float[] GetScoreFourPlayers()
    {
        float[] scores = new float[2];
        int correctGuessesPair1 = 0;
        int correctGuessesPair2 = 0;
        int numberQuestions = GameManager.GM.currentQuestionIndex + 1; // because currentQuestionIndex starts at 0
        for (int i = 0; i < numberQuestions; i++)
        {

            if (answersPlayers[i][0] == answersPlayers[i][1])
            {
                correctGuessesPair1++;
            }
            if (answersPlayers[i][2] == answersPlayers[i][3])
            {
                correctGuessesPair2++;
            }

        }
        scores[0] = correctGuessesPair1 / numberQuestions * 100.0f;
        scores[1] = correctGuessesPair2 / numberQuestions * 100.0f;
        return scores; 
    }

    public void SaveAnswers(int currentQuestionIndex, int[] choicePlayers) {
        for (int i = 0; i < choicePlayers.Length; i++)
        {
            answersPlayers[currentQuestionIndex][i] = choicePlayers[i];
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
            scores[0] = GetScoreTwoPlayers();
        } else if (GameManager.GM.numberPlayers == 4)
        {
            scores = GetScoreFourPlayers();
        }
    }
}
