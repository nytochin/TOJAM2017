using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour {

    public List<GameObject> questions;

    private List<GameObject> questionsLeft;
    private int[][] answersPlayers = null; // [number of questions][number of players]

    public void InitializeQuestions()
    {
        questionsLeft = questions;
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
        int numberQuestions = answersPlayers.GetLength(0);
        for (int i = 0; i < numberQuestions; i++)
        {
            
            if (answersPlayers[i][0] == answersPlayers[i][1])
            {
                correctGuesses++;
            }
            
        }
        return correctGuesses / numberQuestions * 100.0f;
    }

    public float[] GetScoreFourPlayers()
    {
        float[] scores = new float[2];
        int correctGuessesPair1 = 0;
        int correctGuessesPair2 = 0;
        int numberQuestions = answersPlayers.GetLength(0);
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
}
