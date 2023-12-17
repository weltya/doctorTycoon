using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //BUILD 
    [SerializeField] private Button BuildButton;
    [SerializeField] private GameObject SelectionWhichTypeRoom;
    [SerializeField] private GameObject SelectionBuildingReception;
    [SerializeField] private GameObject SelectionBuildingWaiting;
    [SerializeField] private GameObject SelectionBuildingNurse;
    [SerializeField] private GameObject SelectionBuildingDoctor;



    public void NoDisplayAllBuildPanel()
    {
        SelectionBuildingReception.SetActive(false);
        SelectionBuildingWaiting.SetActive(false);
        SelectionBuildingNurse.SetActive(false);
        SelectionBuildingDoctor.SetActive(false);
        SelectionWhichTypeRoom.SetActive(false);
    }
    public void DisplaySelectionWhichTypeRoom()
    {
        SelectionWhichTypeRoom.SetActive(!SelectionWhichTypeRoom.activeSelf);
        if (!SelectionWhichTypeRoom.activeSelf)
        {
            NoDisplayAllBuildPanel();
        }
    }
    public void DisplaySelectionBuildingReception()
    {
        SelectionBuildingWaiting.SetActive(false);
        SelectionBuildingNurse.SetActive(false);
        SelectionBuildingDoctor.SetActive(false);

        SelectionBuildingReception.SetActive(!SelectionBuildingReception.activeSelf);
    }
    public void DisplaySelectionBuildingWaiting()
    {
        SelectionBuildingReception.SetActive(false);
        SelectionBuildingNurse.SetActive(false);
        SelectionBuildingDoctor.SetActive(false);

        SelectionBuildingWaiting.SetActive(!SelectionBuildingWaiting.activeSelf);
    }
    public void DisplaySelectionBuildingNurse()
    {
        SelectionBuildingWaiting.SetActive(false);
        SelectionBuildingReception.SetActive(false);
        SelectionBuildingDoctor.SetActive(false);

        SelectionBuildingNurse.SetActive(!SelectionBuildingNurse.activeSelf);
    }
    public void DisplaySelectionBuildingDoctor()
    {
        SelectionBuildingWaiting.SetActive(false);
        SelectionBuildingNurse.SetActive(false);
        SelectionBuildingReception.SetActive(false);

        SelectionBuildingDoctor.SetActive(!SelectionBuildingDoctor.activeSelf);
    }
}
