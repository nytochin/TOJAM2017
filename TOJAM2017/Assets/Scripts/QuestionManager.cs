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

    public float getScore(List<string> answersP1, List<string> answersP2)
    {
        int correctGuesses = 0;
        for (int i = 0; i < answersP1.Count; i++)
        {
            if (answersP1[i] == answersP2[i])
            {
                correctGuesses++;
            }
        }
        return correctGuesses / answersP1.Count;
    }

    public void SaveAnswers(int currentQuestionIndex, int[] choicePlayers) {
        for (int i = 0; i < choicePlayers.Length; i++)
        {
            answersPlayers[currentQuestionIndex][i] = choicePlayers[i];
        }
    }
}
