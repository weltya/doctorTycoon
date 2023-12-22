using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @class WaitingMenu
 * @brief Manages the waiting menu in the game.
 */
public class WaitingMenu : MonoBehaviour
{
    public GameObject SalleAtPanel; 
    public GameObject SalleInfPanel;
    public GameObject SalleMedPanel;
    /**
     * @brief Start is called before the first frame update.
     * Sets the initial state, making the Reception panel inactive.
     */
    void Start()
    {
      SalleAtPanel.SetActive(false);  
    }

    /**
     * @brief Called when a button in the UI is clicked.
     * Toggles the visibility of the panels based on the current state.
     */
    public void OnButtonClick()
    {
        // Toggle the visibility of the game object
        SalleInfPanel.SetActive(false);
        SalleMedPanel.SetActive(false);
        SalleAtPanel.SetActive(!SalleAtPanel.activeSelf);
    }
}
