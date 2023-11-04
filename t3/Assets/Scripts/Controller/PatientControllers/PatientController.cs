using Model.Caracters.CaractersInMap;
using Model.Caracters.Patients;
using Model.Waypoint;
using Model.Waypoints;
using System.Collections.Generic;
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
            WaypointWaitingRoomNurseModel waypointWaitingRoomNurseModel = WaypointWaitingRoomNurseModel.GetInstance();
            HashSet<PatientDataModel> patientsDataModel = _caractersInMap.GetPatientsInReception();
            List<PatientDataModel> patientsToRemove = new List<PatientDataModel>();

            foreach (var patientData in patientsDataModel)
            {
                patientsToRemove.Add(patientData);
                _caractersInMap.AddPatientInWaitingRoomNursesList(patientData);
                Transform waypoint = waypointWaitingRoomNurseModel.RequestChair();
                patientData.SetTargetChair(waypoint);
            }
            foreach (var patient in patientsToRemove)
            {
                _caractersInMap.RemovePatientFromReceptionList(patient);
            }
        }

        public void MovePatientToRoomNurse()
        {
            WaypointRoomNurseModel waypointRoomNurseModel = WaypointRoomNurseModel.GetInstance();
            HashSet<PatientDataModel> patientsDataModel = _caractersInMap.GetPatientsInWaitingRoomNurse();
            List<PatientDataModel> patientsToRemove = new List<PatientDataModel>();

            foreach (var patientData in patientsDataModel)
            {
                patientsToRemove.Add(patientData);
                _caractersInMap.AddPatientInNursesRoomList(patientData);
                Transform waypoint = waypointRoomNurseModel.RequestChair();
                patientData.SetTargetChair(waypoint);
            }
            foreach (var patient in patientsToRemove)
            {
                _caractersInMap.RemovePatientFromWaitingRoomNursesList(patient);
            }
        }

        public void MovePatientToWaitingRoomDoctor()
        {
            WaypointWaitingRoomDoctorModel waypointWaitingRoomDoctorModel = WaypointWaitingRoomDoctorModel.GetInstance();
            HashSet<PatientDataModel> patientsDataModel = _caractersInMap.GetPatientsInRoomNurse();
            List<PatientDataModel> patientsToRemove = new List<PatientDataModel>();

            foreach (var patientData in patientsDataModel)
            {
                patientsToRemove.Add(patientData);
                _caractersInMap.AddPatientInWaitingDoctorsRoom(patientData);
                Transform waypoint = waypointWaitingRoomDoctorModel.RequestChair();
                patientData.SetTargetChair(waypoint);
            }
            foreach (var patient in patientsToRemove)
            {
                _caractersInMap.RemovePatientFromNursesRoomList(patient);
            }
        }

        public void MovePatientToDoctorRoom()
        {
            WaypointRoomDoctorModel waypointRoomDoctorModel = WaypointRoomDoctorModel.GetInstance();
            HashSet<PatientDataModel> patientsDataModel = _caractersInMap.GetPatientsInWaitingRoomDoctor();
            List<PatientDataModel> patientsToRemove = new List<PatientDataModel>();

            foreach (var patientData in patientsDataModel)
            {
                patientsToRemove.Add(patientData);
                _caractersInMap.AddPatientInDoctorsRoomList(patientData);
                Transform waypoint = waypointRoomDoctorModel.RequestChair();
                patientData.SetTargetChair(waypoint);
            }
            foreach (var patient in patientsToRemove)
            {
                _caractersInMap.RemovePatientFromWaitingRoomDoctorsList(patient);
            }
        }

        public void MovePatientToFinish()
        {
            WaypointFinishModel waypointFinish = WaypointFinishModel.GetInstance();
            HashSet<PatientDataModel> patientsDataModel = _caractersInMap.GetPatientsInRoomDoctor();
            List<PatientDataModel> patientsToRemove = new List<PatientDataModel>();

            foreach (var patientData in patientsDataModel)
            {
                patientsToRemove.Add(patientData);
                _caractersInMap.AddPatientInFinishList(patientData);
                Transform waypoint = waypointFinish.RequestChair();
                patientData.SetTargetChair(waypoint);
            }
            foreach (var patient in patientsToRemove)
            {
                _caractersInMap.RemovePatientFromDoctorsRoomList(patient);
            }
        }
    }
}

