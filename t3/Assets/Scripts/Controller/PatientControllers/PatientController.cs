using System.Collections.Generic;
using UnityEngine;

using View.Caracters;
using View.Caracters.Patients;
using Model.Caracters.Patients;
using Model.Waypoints;
using Model.Characters.CaractersInMap;
using Model.Utils;

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
            _caractersInMap = CaractersInMapModel.GetInstance();
            _caractersInMap.SubscribeToObserverPatient(_instantiatePatientView);
            _patientNavMeshView = patientNavMeshView;
        }

        public void CreatePatient()
        {
            _patientDataModel = new PatientDataModel();
            _caractersInMap.AddPatientInList(PatientState.Spawn, _patientDataModel);
            _patientDataModel.SubscribeToObserverPatient(_patientNavMeshView);
        }

        public void MovePatient(PatientState state)
        {
            WaypointModel waypointsModel = WaypointModel.GetInstance();
            HashSet<PatientDataModel> patientsDataModels = _caractersInMap.GetPatientsByStateFromList(state-1);
            List<PatientDataModel> patientsToRemove = new List<PatientDataModel>();

            foreach(var patientData in patientsDataModels)
            {
                patientsToRemove.Add(patientData);
                _caractersInMap.AddPatientInList(state, patientData);
                Transform waypoint = waypointsModel.RequestWaypoint(state);
                patientData.SetTargetChair(waypoint);
            }

            foreach (var patient in patientsToRemove)
            {
                _caractersInMap.RemovePatientFromList(state - 1, patient);
            }
        }
        public void MovePatientToReception()
        {
            MovePatient(PatientState.Reception);
        }

        public void MovePatientToWaitingRoomNurse()
        {
            MovePatient(PatientState.WaitingRoomNurse);
        }

        public void MovePatientToRoomNurse()
        {
            MovePatient(PatientState.NurseRoom);
        }

        public void MovePatientToWaitingRoomDoctor()
        {
            MovePatient(PatientState.WaitingRoomDoctor);
        }

        public void MovePatientToDoctorRoom()
        {
            MovePatient(PatientState.DoctorRoom);
        }

        public void MovePatientToFinish()
        {
            MovePatient(PatientState.Finish);
        }
    }
}