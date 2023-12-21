using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnMenuButtonClicked()
    {
        // Load the scene with the game (replace "GameScene" with your actual scene name)
        SceneManager.LoadScene("MainMenu");
    }
}
