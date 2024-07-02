using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private QuizUI quizUI;

    // Start is called before the first frame update
    public void StartNext()
    {
        Button nextButton = GetComponent<Button>();
        //nextButton.onClick.AddListener(quizUI.OnClickNext);

    }

   // create an array to save the time of next button, and use playerpref .Get like before
}


