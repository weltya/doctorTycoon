using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @class MainMenuExit
 * @brief Handles the exit functionality in the main menu.
 */
public class MainMenuExit : MonoBehaviour
{
    /**
     * @brief Called when the exit button is clicked.
     * Quits the application (works in standalone builds, not in the Unity Editor).
     */
     public void OnExitButtonClicked()
    {
        // Quit the application (works in standalone builds, not in the Unity Editor)
        Application.Quit();
    }
}
