using Model.Waypoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Waypoints
{
    public class WaypointWaitingRoom : MonoBehaviour
    {
        private void Start()
        {
            InitializeWaypoints();
        }
        private void InitializeWaypoints()
        {
            GameObject waypointsParent = GameObject.Find("WaypointsWaitingRoom");
            if (waypointsParent != null)
            {
                foreach (Transform waypoint in waypointsParent.transform)
                {
                    WaypointWaitingRoomModel.GetInstance().AddWaypoint(waypoint);
                }
            }
            else
            {
                Debug.LogError("WaypointsWaitingRoom GameObject not found in the scene.");
            }
        }
    }
}

