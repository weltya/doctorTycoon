using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorMenu : MonoBehaviour
{
    public GameObject SalleMedPanel; 
    public GameObject SalleInfPanel;
    public GameObject SalleAtPanel;
    // Start is called before the first frame update
    void Start()
    {
      SalleMedPanel.SetActive(false);  
    }

    // Update is called once per frame
    public void OnButtonClick()
    {
        // Toggle the visibility of the game object
        SalleAtPanel.SetActive(false);
        SalleInfPanel.SetActive(false);
        SalleMedPanel.SetActive(!SalleMedPanel.activeSelf);
    }
}
