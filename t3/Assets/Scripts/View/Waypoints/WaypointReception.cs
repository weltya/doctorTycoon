using Model.Waypoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Waypoints
{
    public class WaypointReception : MonoBehaviour
    {
        private void Start()
        {
            InitializeWaypoints();
        }
        private void InitializeWaypoints()
        {
            GameObject waypointsParent = GameObject.Find("WaypointsReception");
            if (waypointsParent != null)
            {
                foreach (Transform waypoint in waypointsParent.transform)
                {
                    WaypointWaitingRoomModel.GetInstance().AddWaypoint(waypoint);
                    Debug.Log("Waypoint chargé");
                }
            }
            else
            {
                Debug.LogError("WaypointsWaitingRoom GameObject not found in the scene.");
            }
        }
    }
}

