using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @class BuildMenu
 * @brief Manages the build menu in the game.
 */
public class BuildMenu : MonoBehaviour
{

    public GameObject BuildPanel2;
    public GameObject SalleAtPanel; 
    public GameObject SalleInfPanel;
    public GameObject SalleMedPanel;
    /**
     * @brief Called when the script instance is being loaded.
     * Initializes the state of the build menu.
     */
    void Start()
    {
            BuildPanel2.SetActive(false);
    }

     /**
     * @brief Called when the button is clicked to toggle the visibility of the build menu.
     */
    public void OnButtonClick()
    {
        // Toggle the visibility of the game object
        SalleAtPanel.SetActive(false);
        SalleInfPanel.SetActive(false);
        SalleMedPanel.SetActive(false);
        BuildPanel2.SetActive(!BuildPanel2.activeSelf);
    }
}
