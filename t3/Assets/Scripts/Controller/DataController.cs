using System.Collections.Generic;
using UnityEngine;

using Model.Waypoints;
using Model.Caracters;
using Model.Utils;

namespace Controller
{
    public class DataController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _patientPrefabs = new List<GameObject>();

        private readonly List<(string, IWaypointModel, PatientState)> _waypointsInitializationList = new List<(string, IWaypointModel, PatientState)>
        {
            ("WaypointsReception", WaypointModel.GetInstance(), PatientState.Reception),
            ("WaypointsWaitingRoomNurse", WaypointModel.GetInstance(), PatientState.WaitingRoomNurse),
            ("WaypointNurseRoom", WaypointModel.GetInstance(), PatientState.NurseRoom),
            ("WaypointWaitingRoomDoctor", WaypointModel.GetInstance(), PatientState.WaitingRoomDoctor),
            ("WaypointRoomDoctor", WaypointModel.GetInstance(), PatientState.DoctorRoom),
            ("WaypointFinish", WaypointModel.GetInstance(), PatientState.Finish)
        };

        private void Start()
        {
            foreach (var (parentName, model, state) in _waypointsInitializationList)
            {
                InitializeWaypoints(parentName, model, state);
            }
            InitializePatientPrefab();
        }

        public void InitializePatientPrefab()
        {
            CaractersPrefabModel.GetInstance().SetPatientsPrefabs(_patientPrefabs);
        }

        private void InitializeWaypoints(string waypointsParentName, IWaypointModel waypointModel, PatientState state)
        {
            GameObject waypointsParent = GameObject.Find(waypointsParentName);
            if (waypointsParent != null)
            {
                foreach (Transform waypoint in waypointsParent.transform)
                {
                    waypointModel.AddWaypoint(state, waypoint);
                }
            }
            else
            {
                Debug.LogError($"{waypointsParentName} GameObject not found in the scene.");
            }
        }
    }
}