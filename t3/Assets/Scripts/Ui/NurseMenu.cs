using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @class NurseMenu
 * @brief Manages the nurse menu in the game.
 */
public class NurseMenu : MonoBehaviour
{
     public GameObject SalleInfPanel;
     public GameObject SalleMedPanel;
     public GameObject SalleAtPanel; 
    // Start is called before the first frame update
    void Start()
    {
      SalleInfPanel.SetActive(false);  
    }

    /**
     * @brief Toggles the visibility of the nurse menu panels.
     */
    public void OnButtonClick()
    {
        // Toggle the visibility of the game object
        SalleAtPanel.SetActive(false);
        SalleMedPanel.SetActive(false);
        SalleInfPanel.SetActive(!SalleInfPanel.activeSelf);
    }
}
