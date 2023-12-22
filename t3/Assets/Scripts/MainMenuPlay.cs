using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * @class MainMenuPlay
 * @brief Handles the play functionality in the main menu.
 */
public class MainMenuPlay : MonoBehaviour
{
    /**
     * @brief Called when the play button is clicked.
     * Loads the scene with the game (replace "MapScene" with your actual scene name).
     */
    public void OnPlayButtonClicked()
    {
        // Load the scene with the game (replace "GameScene" with your actual scene name)
        SceneManager.LoadScene("MapScene");
    }
}

