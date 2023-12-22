using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @class DoctorMenu
 * @brief Manages the doctor's menu in the game.
 */
public class DoctorMenu : MonoBehaviour
{
    public GameObject SalleMedPanel; 
    public GameObject SalleInfPanel;
    public GameObject SalleAtPanel;

    /**
     * @brief Called when the script instance is being loaded.
     * Initializes the state of the doctor's menu.
     */    
    void Start()
    {
      SalleMedPanel.SetActive(false);  
    }

    /**
     * @brief Called when the button is clicked to toggle the visibility of the doctor's menu.
     */
    public void OnButtonClick()
    {
        // Toggle the visibility of the game object
        SalleAtPanel.SetActive(false);
        SalleInfPanel.SetActive(false);
        SalleMedPanel.SetActive(!SalleMedPanel.activeSelf);
    }
}
