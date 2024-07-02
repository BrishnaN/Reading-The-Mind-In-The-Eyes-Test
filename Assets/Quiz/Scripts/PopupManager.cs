using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel; // Reference to the pop-up panel
    public GameObject endPanel; // Reference to the End panel
   
    void Start()
    {
                                    
    }

   

    // Method to show the pop-up
    public void ShowPopup()
    {
        endPanel.SetActive(false); // Hide the End panel
        popupPanel.SetActive(true);
    }

    // Method to hide the pop-up
    public void HidePopup()
    {
        popupPanel.SetActive(false);
        endPanel.SetActive(true);   // Show the End panel
    }
}