using Model.Patients;
using Model.Waypoints;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using View.Caracters;
using View.Caracters.Patients;

namespace Controller.PatientControllers
{
    public class PatientController
    {
        private PatientDataModel _patientDataModel;
        private CaractersInMapModel _caractersInMap;
        private InstantiatePatientView _instantiatePatientView;
        private PatientNavMeshView _patientNavMeshView;
        public PatientController(InstantiatePatientView instantiatePatientView, PatientNavMeshView patientNavMeshView)
        {
            
            _instantiatePatientView = instantiatePatientView;
            _caractersInMap = CaractersInMapModel.GetIntance();
            _caractersInMap.SubscribeToObserverPatient(_instantiatePatientView);
            _patientNavMeshView = patientNavMeshView;
        }

        public void CreatePatient()
        {
            _patientDataModel = new PatientDataModel();
            _caractersInMap.AddPatientToWaitingRoom(_patientDataModel);
            _patientDataModel.SubscribeToObserverPatient(_patientNavMeshView);
        }

        public void AssignWaypointsToPatientsGoingWaitingRoom()
        {
            WaypointWaitingRoomModel waypointWaitingRoomModel = WaypointWaitingRoomModel.GetInstance();

            HashSet<PatientDataModel> patientsDataModel = _caractersInMap.GetPatientsWaitingRoom();

            foreach(var patientData in patientsDataModel)
            {
                Transform waypoint = waypointWaitingRoomModel.RequestChair();
                patientData.SetTargetChair(waypoint);
            }
        }
    }
}

