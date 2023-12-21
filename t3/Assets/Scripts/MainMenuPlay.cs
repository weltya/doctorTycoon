using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPlay : MonoBehaviour
{
    
    public void OnPlayButtonClicked()
    {
        // Load the scene with the game (replace "GameScene" with your actual scene name)
        SceneManager.LoadScene("MapScene");
    }
}

