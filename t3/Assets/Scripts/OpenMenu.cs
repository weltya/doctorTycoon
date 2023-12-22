using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/**
 * @class OpenMenu
 * @brief Handles the opening of the main menu.
 */
public class OpenMenu : MonoBehaviour
{
    /**
     * @brief Called when the menu button is clicked.
     * Loads the main menu scene (replace "MainMenu" with your actual scene name).
     */
    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
