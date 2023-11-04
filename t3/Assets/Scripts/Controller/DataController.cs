using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Model.Waypoints;
using Model.Caracters;
using Model.Waypoint;

namespace Controller
{
    public class DataController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _patientPrefabs = new List<GameObject>();

        private void Start()
        {
            InitializeWaypointsReception();
            InitializeWaypointsWaitingRoomNurse();
            InitializeWaypointsRoomNurse();
            InitializeWaypointsWaitingRoomDoctor();
            InitializeWaypointsRoomDoctor();
            InitializeWaypointsFinish();
            InializePatientPrefab();
        }

        public List<GameObject> InializePatientPrefab()
        {
            CaractersPrefabModel patientPrefab = CaractersPrefabModel.GetInstance();
            patientPrefab.SetPatientsPrefabs(_patientPrefabs);

            return _patientPrefabs;
        }

        private void InitializeWaypointsReception()
        {
            GameObject waypointsParent = GameObject.Find("WaypointsReception");
            if (waypointsParent != null)
            {
                foreach (Transform waypoint in waypointsParent.transform)
                {
                    WaypointReceptionModel.GetInstance().AddWaypoint(waypoint);
                }
            }
            else
            {
                Debug.LogError("WaypointsWaitingRoom GameObject not found in the scene.");
            }
        }

        private void InitializeWaypointsWaitingRoomNurse()
        {
            GameObject waypointsParent = GameObject.Find("WaypointsWaitingRoomNurse");
            if (waypointsParent != null)
            {
                foreach (Transform waypoint in waypointsParent.transform)
                {
                    WaypointWaitingRoomNurseModel.GetInstance().AddWaypoint(waypoint);
                }
            }
            else
            {
                Debug.LogError("WaypointsWaitingRoom GameObject not found in the scene.");
            }
        }

        private void InitializeWaypointsRoomNurse()
        {
            GameObject waypointsParent = GameObject.Find("WaypointNurseRoom");
            if (waypointsParent != null)
            {
                foreach (Transform waypoint in waypointsParent.transform)
                {
                    WaypointRoomNurseModel.GetInstance().AddWaypoint(waypoint);
                }
            }
            else
            {
                Debug.LogError("WaypointsWaitingRoom GameObject not found in the scene.");
            }
        }

        private void InitializeWaypointsWaitingRoomDoctor()
        {
            GameObject waypointsParent = GameObject.Find("WaypointWaitingRoomDoctor");
            if (waypointsParent != null)
            {
                foreach (Transform waypoint in waypointsParent.transform)
                {
                    WaypointWaitingRoomDoctorModel.GetInstance().AddWaypoint(waypoint);
                }
            }
            else
            {
                Debug.LogError("WaypointsWaitingRoom GameObject not found in the scene.");
            }
        }

        private void InitializeWaypointsRoomDoctor()
        {
            GameObject waypointsParent = GameObject.Find("WaypointRoomDoctor");
            if (waypointsParent != null)
            {
                foreach (Transform waypoint in waypointsParent.transform)
                {
                    WaypointRoomDoctorModel.GetInstance().AddWaypoint(waypoint);
                }
            }
            else
            {
                Debug.LogError("WaypointsWaitingRoom GameObject not found in the scene.");
            }
        }
        private void InitializeWaypointsFinish()
        {
            GameObject waypointsParent = GameObject.Find("WaypointFinish");
            if (waypointsParent != null)
            {
                foreach (Transform waypoint in waypointsParent.transform)
                {
                    WaypointFinishModel.GetInstance().AddWaypoint(waypoint);
                }
            }
            else
            {
                Debug.LogError("WaypointsWaitingRoom GameObject not found in the scene.");
            }
        }
    }
}

