using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * @class UI
 * @brief Manages the user interface in the game.
 * 
 * This class is responsible for handling UI elements, such as buttons and panels, related to building and room selection.
 */
public class UI : MonoBehaviour
{
    //BUILD 
    [SerializeField] private Button BuildButton;
    [SerializeField] private GameObject SelectionWhichTypeRoom;
    [SerializeField] private GameObject SelectionBuildingReception;
    [SerializeField] private GameObject SelectionBuildingWaiting;
    [SerializeField] private GameObject SelectionBuildingNurse;
    [SerializeField] private GameObject SelectionBuildingDoctor;


    /**
     * @brief Hides all building-related panels.
     */
    public void NoDisplayAllBuildPanel()
    {
        SelectionBuildingReception.SetActive(false);
        SelectionBuildingWaiting.SetActive(false);
        SelectionBuildingNurse.SetActive(false);
        SelectionBuildingDoctor.SetActive(false);
        SelectionWhichTypeRoom.SetActive(false);
    }

    /**
     * @brief Toggles the visibility of the room type selection panel.
     */
    public void DisplaySelectionWhichTypeRoom()
    {
        SelectionWhichTypeRoom.SetActive(!SelectionWhichTypeRoom.activeSelf);
        if (!SelectionWhichTypeRoom.activeSelf)
        {
            NoDisplayAllBuildPanel();
        }
    }

     /**
     * @brief Toggles the visibility of the reception room building panel.
     */
    public void DisplaySelectionBuildingReception()
    {
        SelectionBuildingWaiting.SetActive(false);
        SelectionBuildingNurse.SetActive(false);
        SelectionBuildingDoctor.SetActive(false);

        SelectionBuildingReception.SetActive(!SelectionBuildingReception.activeSelf);
    }

    /**
     * @brief Toggles the visibility of the waiting room building panel.
     */
    public void DisplaySelectionBuildingWaiting()
    {
        SelectionBuildingReception.SetActive(false);
        SelectionBuildingNurse.SetActive(false);
        SelectionBuildingDoctor.SetActive(false);

        SelectionBuildingWaiting.SetActive(!SelectionBuildingWaiting.activeSelf);
    }

    /**
     * @brief Toggles the visibility of the nurse room building panel.
     */
    public void DisplaySelectionBuildingNurse()
    {
        SelectionBuildingWaiting.SetActive(false);
        SelectionBuildingReception.SetActive(false);
        SelectionBuildingDoctor.SetActive(false);

        SelectionBuildingNurse.SetActive(!SelectionBuildingNurse.activeSelf);
    }

    /**
     * @brief Toggles the visibility of the doctor room building panel.
     */
    public void DisplaySelectionBuildingDoctor()
    {
        SelectionBuildingWaiting.SetActive(false);
        SelectionBuildingNurse.SetActive(false);
        SelectionBuildingReception.SetActive(false);

        SelectionBuildingDoctor.SetActive(!SelectionBuildingDoctor.activeSelf);
    }
}
