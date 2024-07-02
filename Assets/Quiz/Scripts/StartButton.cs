using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Assuming you are using TextMeshPro for your input fields

public class StartButton : MonoBehaviour
{
    public TMP_InputField input_field;
    public TMP_InputField set_input_field;
   
    public string PlayerName;
    public string SetName = "YY";
    public static StartButton instance2;

    [SerializeField] private QuizManager quizManager;

   public void Start()
    {
        PlayerName = "zz";
        instance2 = this;
    }

    public void StartGame()
    {
        if(input_field.text == "" || set_input_field.text == "")
       
        {
            Debug.LogError("Input fields are not assigned.");
            return;
        }
        PlayerName = input_field.text;
        SetName = set_input_field.text;
        PlayerName = PlayerName;
        PlayerPrefs.SetString("PlayerName", PlayerName);
        PlayerPrefs.SetString("SetName", SetName);
        PlayerPrefs.Save(); 

        //dekhikihoy();
        Debug.Log(PlayerName + " " + SetName);
        SceneManager.LoadScene("GameScene");
        // Call a method in your QuizManager or game manager script to start the game
        //quizManager.StartGame(); // Example assuming QuizManager has a StartGame() method
    }
}
