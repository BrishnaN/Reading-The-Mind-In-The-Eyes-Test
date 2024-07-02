using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitTheGame()
    {
        // TopMenu.SetActive(false);
        Debug.Log("Quiting");
        Application.Quit();
       // Debug.Log("Quiting");
    }
}
