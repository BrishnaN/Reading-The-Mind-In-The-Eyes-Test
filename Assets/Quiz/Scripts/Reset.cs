using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
   


    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        
    }
}