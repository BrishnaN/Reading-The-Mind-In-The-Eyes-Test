using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using System.IO;

public class EndSceneManager : MonoBehaviour
{
    private float startTime;
    private float endTime;

    [SerializeField] public Text scoreText;



    // Start is called before the first frame update
    public void Start()
    {
        // Retrieve player 
        string playerName = PlayerPrefs.GetString("PlayerName", "DefaultPlayerName");
        string setName = PlayerPrefs.GetString("SetName", "DefaultSetName");
        // Debug.Log(playerName + " " + setName);
        Debug.Log("PlayerName: " + playerName);
        Debug.Log("SetName: " + setName);

        // Retrieve start time from PlayerPrefs
        string startTime = PlayerPrefs.GetString("StartTime");
        PlayerPrefs.Save();
        Debug.Log("Start Time: " + startTime);

        //end time
        DateTime currentTime = DateTime.Now;
        string endTimeStr = currentTime.ToString("yyyy-MM-ddTHH:mm:ss.fff");
        Debug.Log("Start Time: " + endTimeStr);
        PlayerPrefs.SetString("EndTime", endTimeStr);
        PlayerPrefs.Save();
        // Retrieve end time from PlayerPrefs
        string endTime = PlayerPrefs.GetString("EndTime");


        // Retrieve time IDs
        int timeId = PlayerPrefs.GetInt("TimeId");
        int timeIdNextButton = PlayerPrefs.GetInt("TimeIdNextButton");
        Debug.Log("Time ID: " + timeId);
        Debug.Log("Time Next Button ID: " + timeIdNextButton);

        // Retrieve check answers
        for (int i = 0; i < 37; i++)
        {
            Debug.Log("End Scene checkans " + PlayerPrefs.GetInt("checkAnswer_" + i));
        }

        // final score
        int finalScore = PlayerPrefs.GetInt("FinalScore");
        Debug.Log("End Scene finalScore " + finalScore);
        scoreText.text = "Final Score:   " + finalScore.ToString() + "/" + 36;




        // quiz data in a CSV file
        string fileName = playerName + setName + "_quiz_data.csv";
        // Write to a CSV file
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);

        System.Text.StringBuilder csvBuilder = new System.Text.StringBuilder();
        // Add Column Headers
        csvBuilder.AppendLine("CheckAnswer,TimeId,TimeIdNextButton");


        for (int i = 0; i < 37; i++)
        {
            int checkAnswer = PlayerPrefs.GetInt("checkAnswer_" + i);
            string dateTimeArrayOptionButton = PlayerPrefs.GetString("dateTimeArrayOptionButton_" + i);
            string dateTimeArrayNextButton = PlayerPrefs.GetString("dateTimeArrayNextButton_" + i);
            string row = $"{checkAnswer},{dateTimeArrayOptionButton},{dateTimeArrayNextButton}";
            csvBuilder.AppendLine(row);
        }


        string file2Name = playerName + setName + "_quiz_data.txt";
        // Write to a txt file
        string file2Path = System.IO.Path.Combine(Application.persistentDataPath, file2Name);

        System.Text.StringBuilder txtBuilder = new System.Text.StringBuilder();
        // Add Column Headers
        txtBuilder.AppendLine("playerName = " + playerName);
        txtBuilder.AppendLine("SetName = " + setName);
        txtBuilder.AppendLine("StartTime = " + startTime);
        txtBuilder.AppendLine("EndTime = " + endTime);
        txtBuilder.AppendLine("FinalScore " + finalScore);


        File.WriteAllText(filePath, csvBuilder.ToString());
        File.WriteAllText(file2Path, txtBuilder.ToString());



    }
    public void QuitTheGame()
    {
        // TopMenu.SetActive(false);
        Application.Quit();
        Debug.Log("Quiting");
    }
}
