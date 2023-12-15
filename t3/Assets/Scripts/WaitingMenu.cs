using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingMenu : MonoBehaviour
{
    public GameObject SalleAtPanel; 
    public GameObject SalleInfPanel;
    public GameObject SalleMedPanel;
    // Start is called before the first frame update
    void Start()
    {
      SalleAtPanel.SetActive(false);  
    }

    // Update is called once per frame
    public void OnButtonClick()
    {
        // Toggle the visibility of the game object
        SalleInfPanel.SetActive(false);
        SalleMedPanel.SetActive(false);
        SalleAtPanel.SetActive(!SalleAtPanel.activeSelf);
    }
}
