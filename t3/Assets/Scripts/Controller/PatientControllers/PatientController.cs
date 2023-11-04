using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

using View.Caracters;
using View.Caracters.Patients;
using Model.Caracters.CaractersInMap;
using Model.Caracters.Patients;
using Model.Waypoints;

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
            _caractersInMap.AddPatientInSpawnList(_patientDataModel);
            _patientDataModel.SubscribeToObserverPatient(_patientNavMeshView);
        }

        public void MovePatientToReception() 
        {
            WaypointReceptionModel waypointReceptionModel = WaypointReceptionModel.GetInstance();
            HashSet<PatientDataModel> patientsDataModels = _caractersInMap.GetPatientsInSpawn();
            List<PatientDataModel> patientsToRemove = new List<PatientDataModel>();

            foreach (var patientData in patientsDataModels) 
            {
                patientsToRemove.Add(patientData);
                _caractersInMap.AddPatientInReceptionList(patientData);
                Transform waypoint = waypointReceptionModel.RequestChair();
                patientData.SetTargetChair(waypoint);
            }
            foreach (var patient in patientsToRemove)
            {
                _caractersInMap.RemovePatientFromSpawnList(patient);
            }
        }

        public void MovePatientToWaitingRoomNurse()
        {
            WaypointWaitingRoomNurseModel waypointWaitingRoomModel = WaypointWaitingRoomNurseModel.GetInstance();
            HashSet<PatientDataModel> patientsDataModel = _caractersInMap.GetPatientsInReception();
            List<PatientDataModel> patientsToRemove = new List<PatientDataModel>();

            foreach (var patientData in patientsDataModel)
            {
                patientsToRemove.Add(patientData);
                _caractersInMap.RemovePatientFromReceptionList(patientData);
                _caractersInMap.AddPatientInWaitingRoomNursesList(patientData);
                Transform waypoint = waypointWaitingRoomModel.RequestChair();
                patientData.SetTargetChair(waypoint);
            }
            foreach (var patient in patientsToRemove)
            {
                _caractersInMap.RemovePatientFromReceptionList(patient);
            }
        }
    }
}

