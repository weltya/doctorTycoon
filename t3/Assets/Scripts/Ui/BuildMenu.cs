using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{

    public GameObject BuildPanel2;
    public GameObject SalleAtPanel; 
    public GameObject SalleInfPanel;
    public GameObject SalleMedPanel;
    // Start is called before the first frame update
    void Start()
    {
            BuildPanel2.SetActive(false);
    }

    // Update is called once per frame
    public void OnButtonClick()
    {
        // Toggle the visibility of the game object
        SalleAtPanel.SetActive(false);
        SalleInfPanel.SetActive(false);
        SalleMedPanel.SetActive(false);
        BuildPanel2.SetActive(!BuildPanel2.activeSelf);
    }
}
