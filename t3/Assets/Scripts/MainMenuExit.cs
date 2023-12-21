using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuExit : MonoBehaviour
{
    
     public void OnExitButtonClicked()
    {
        // Quit the application (works in standalone builds, not in the Unity Editor)
        Application.Quit();
    }
}
