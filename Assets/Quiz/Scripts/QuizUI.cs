using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;



public class QuizUI : MonoBehaviour
{

    [SerializeField] private Text questionCounterText;
    private int currentQuestionIndex = 0;
    private int totalQuestions = 36;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private Text questionText;
    [SerializeField] private Image questionImage;
    [SerializeField] private List<Button> options;
    [SerializeField] private Button nextButton;
    //[SerializeField] private Button resetButton;

    [SerializeField] private Color correctCol, wrongCol, normalCol;

    private Question question;
    private bool answered;
    public int timeId = 0;
    public int timeIdNextButton = 0;
    // LSL information 
    private TcpClient client;
    private NetworkStream stream;
    private bool clicked = false;

    public DateTime[] dateTimeArrayOptionButton = new DateTime[36];
    public DateTime[] dateTimeArrayNextButton = new DateTime[36];
    // Call this method at the start of each question
    // LSL code
    private void StartConnection()
    {
        try
        {
            client = new TcpClient("localhost", 22346);
            stream = client.GetStream();

            SendData("start\n");
        }
        catch (Exception e)
        {
            Debug.LogError("Exception: " + e.Message);
        }
    }

    private void SendData(string message)
    {
        if (stream != null && stream.CanWrite)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
        else
        {
            Debug.LogError("Cannot write to stream or stream is null");
        }
    }

    void OnDestroy()
    {
        CloseConnection();
    }

    private void CloseConnection()
    {
        if (stream != null)
        {
            stream.Close();
        }
        if (client != null)
        {
            client.Close();
        }
    }
    // LSL code

    public void Start()
    {
        // LSL code
        // StartConnection();
        string playerName = PlayerPrefs.GetString("PlayerName", "DefaultPlayerName");
        Debug.Log("PlayerName retrieved in QuizUI: " + playerName);
        // Add listeners for each option button
        foreach (Button optionButton in options)
        {
            optionButton.onClick.AddListener(() => onClick(optionButton));
        }

        // Add listener for the Next button
        nextButton.onClick.AddListener(OnClickNext);


    }



    public void SetQuestion(Question question)
    {
        currentQuestionIndex++;
        questionCounterText.text = currentQuestionIndex + " / " + totalQuestions;

        this.question = question;
        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImage.transform.parent.gameObject.SetActive(false);
                break;
            case QuestionType.IMAGE:
                //ImageHolder();
                questionImage.transform.gameObject.SetActive(true);
                questionImage.gameObject.SetActive(true); // Make sure image object itself is active;
                questionImage.sprite = question.questionImg;
                break;

        }

        questionText.text = question.questionInfo;
        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<Text>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = normalCol;
        }

        answered = false;
        nextButton.interactable = false;
        UpdateQuestionCounter();



    }




    private void onClick(Button btn)
    {
        if (clicked)
        {
            return;
        }
        clicked = true;
        DateTime currentTime = DateTime.Now;
        dateTimeArrayOptionButton[timeId] = currentTime;
        Debug.Log("Button has been pressed " + dateTimeArrayOptionButton[timeId]);
        timeId++;
        // Store timeId
        PlayerPrefs.SetInt("TimeId", timeId);
        // save time here as well. 
        if (!answered)
        {
            answered = true;
            bool val = quizManager.Answer(btn.name);

            if (val)
            {
                btn.image.color = correctCol;
            }
            else
            {
                btn.image.color = wrongCol;
                // Highlight the correct answer
                foreach (Button optionButton in options)
                {
                    if (optionButton.name == question.correctAns)
                    {
                        optionButton.image.color = correctCol;
                        break;
                    }
                }
            }
            nextButton.interactable = true; // Enable the next button
        }
    }

    public void OnClickNext()
    {
        clicked = false;
        DateTime currentTime = DateTime.Now;
        dateTimeArrayNextButton[timeIdNextButton] = currentTime;
        Debug.Log("Next Button pressed Time" + dateTimeArrayNextButton[timeIdNextButton]);
        timeIdNextButton++;
        // Store timeIdNextButton
        PlayerPrefs.SetInt("TimeIdNextButton", timeIdNextButton);
        quizManager.SelectQuestion();

    }
    public void OnClickReset()
    {
        quizManager.ResetGame(); // Call QuizManager's reset method
        ResetUI(); // Reset UI elements
    }

    public void ResetUI()
    {
        questionCounterText.text = "0 / " + totalQuestions;
        questionText.text = "";
        questionImage.gameObject.SetActive(false);
        foreach (Button optionButton in options)
        {
            optionButton.GetComponentInChildren<Text>().text = "";
            optionButton.image.color = normalCol;
        }
    }

    private void UpdateQuestionCounter()
    {
        totalQuestions = quizManager.quizData.questions.Count;
        //  int currentQuestionIndex = quizManager.CurrentQuestionIndex;
        questionCounterText.text = currentQuestionIndex + " / " + totalQuestions;
    }

}








