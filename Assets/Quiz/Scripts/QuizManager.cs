using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] public QuizDataScriptTable quizData;

    private List<Question> questions;
    public static Question selectedQuestion;
    private int currentQuestionIndex = 0;
    public int score = 0;
    public string startTime;

    public static int[] CheckAnswerarray = new int[36]; // Array with 36 elements initialized to 0
    public static int CheckAnswerId = 0;

    // Start is called before the first frame update
    public void Start()
    {
        DateTime currentTime1 = DateTime.Now;
        startTime = currentTime1.ToString("yyyy-MM-ddTHH:mm:ss.fff");
        PlayerPrefs.SetString("StartTime", startTime);
        PlayerPrefs.Save();
        Debug.Log("Start Time: " + startTime);
        questions = quizData.questions;

        SelectQuestion();
    }

    public void ResetQuiz()
    {
        currentQuestionIndex = 0;
        score = 0;

    }



    public void StartGame()
    {
        ResetQuiz();
        SelectQuestion();
    }


    public void SelectQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        //if (currentQuestionIndex < 5)
        {
            selectedQuestion = questions[currentQuestionIndex];
            quizUI.SetQuestion(selectedQuestion);
            //quizUI.SetQuestion(questions[currentQuestionIndex]);
            currentQuestionIndex++;
        }
        else
        {
            Debug.Log("final Score before end " + score);
            PlayerPrefs.SetInt("FinalScore", score); // Store the score
            PlayerPrefs.Save(); // Ensure the data is saved

            for (int i = 0; i < 5; i++)
            {
                PlayerPrefs.SetInt("checkAnswer_" + i, CheckAnswerarray[i]);
                PlayerPrefs.SetString("dateTimeArrayOptionButton_" + i, quizUI.dateTimeArrayOptionButton[i].ToString("yyyy-MM-ddTHH:mm:ss.fff"));
                PlayerPrefs.SetString("dateTimeArrayNextButton_" + i, quizUI.dateTimeArrayNextButton[i].ToString("yyyy-MM-ddTHH:mm:ss.fff"));
            }

            //Debug.Log(quizUI.dateTimeArrayOptionButton[0]);
            SceneManager.LoadScene("End");

        }



    }

    public bool Answer(string answered)

    {
        bool correctAns = false;
        if (answered == selectedQuestion.correctAns)
        {
            correctAns = true;
            score++;
            CheckAnswerarray[CheckAnswerId] = 1;
        }
        else
            CheckAnswerarray[CheckAnswerId] = 0;
        Debug.Log("Question" + CheckAnswerId + " =" + CheckAnswerarray[CheckAnswerId]);
        CheckAnswerId++;


        //Invoke("SelectQuestion", 0.4f);
        return correctAns;
    }

    public void ResetGame()
    {

        ResetQuiz();

    }
}


[System.Serializable]

public class Question
{
    public string questionInfo;
    public QuestionType questionType;
    public Sprite questionImg;
    public List<string> options;
    public string correctAns;
}

[System.Serializable]

public enum QuestionType
{
    IMAGE,
    TEXT

}

