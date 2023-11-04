using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Model.Caracters.Patients;


namespace Model.Caracters.CaractersInMap
{
    public class CaractersInMapModel
    {
        private static CaractersInMapModel _instance;

        private readonly HashSet<PatientDataModel> _patientsInSpawn;
        private readonly HashSet<PatientDataModel> _patientsInReception;
        private readonly HashSet<PatientDataModel> _patientsInWaitingRoomNurse;
        private readonly HashSet<PatientDataModel> _patientInNursesRoom;
        private readonly HashSet<PatientDataModel> _patientsInWaitingDoctorRoom;
        private readonly HashSet<PatientDataModel> _patientsInDoctorsRoom;
        private readonly HashSet<PatientDataModel> _patientsFinish;
        private readonly List<IObserverCaractersInMap> _observersPatients;
        private CaractersInMapModel()
        {
            _patientsInSpawn = new HashSet<PatientDataModel>();
            _patientsInReception = new HashSet<PatientDataModel>();
            _patientsInWaitingRoomNurse = new HashSet<PatientDataModel>();
            _patientsInWaitingRoomNurse = new HashSet<PatientDataModel>();
            _patientInNursesRoom = new HashSet<PatientDataModel>();
            _patientsInWaitingDoctorRoom = new HashSet<PatientDataModel>();
            _patientsInDoctorsRoom = new HashSet<PatientDataModel>();
            _patientsFinish = new HashSet<PatientDataModel>();
            _observersPatients = new List<IObserverCaractersInMap>();
        }

        public static CaractersInMapModel GetIntance()
        {
            if (_instance == null)
            {
                _instance = new CaractersInMapModel();
            }
            return _instance;
        }

        public HashSet<PatientDataModel> GetPatientsInSpawn()
        {
            return _patientsInSpawn;
        }

        public HashSet<PatientDataModel> GetPatientsInReception()
        {
            return _patientsInReception;
        }

        public void AddPatientInSpawnList(PatientDataModel patient)
        {
            _patientsInSpawn.Add(patient);
            notifyObserverCreationPatients(patient);
        }

        public void AddPatientInReceptionList(PatientDataModel patient)
        {
            _patientsInReception.Add(patient);
        }
        public void AddPatientInWaitingRoomNursesList(PatientDataModel patient)
        {
            _patientsInWaitingRoomNurse.Add(patient);
        }
        public void AddPatientInNursesRoomList(PatientDataModel patient)
        {
            _patientInNursesRoom.Add(patient);
        }
        public void AddPatientInWaitingDoctorsRoom(PatientDataModel patient)
        {
            _patientsInWaitingDoctorRoom.Add(patient);
        }
        public void AddPatientInDoctorsRoomList(PatientDataModel patient)
        {
            _patientsInDoctorsRoom.Add(patient);
        }
        public void AddPatientInFinishList(PatientDataModel patient)
        {
            _patientsFinish.Add(patient);
        }


        public void RemovePatientFromSpawnList(PatientDataModel patient)
        {
            _patientsInSpawn.Remove(patient);
        }
        public void RemovePatientFromReceptionList(PatientDataModel patient)
        {
            _patientsInSpawn.Remove(patient);
        }
        public void RemovePatientFromWaitingRoomNursesList(PatientDataModel patient)
        {
            _patientsInSpawn.Remove(patient);
        }
        public void RemovePatientFromNursesRoomList(PatientDataModel patient)
        {
            _patientsInSpawn.Remove(patient);
        }
        public void RemovePatientFromDoctorsRoom(PatientDataModel patient)
        {
            _patientsInSpawn.Remove(patient);
        }
        public void RemovePatientFromDoctorsRoomList(PatientDataModel patient)
        {
            _patientsInSpawn.Remove(patient);
        }
        public void RemovePatientFromFinishList(PatientDataModel patient)
        {
            _patientsInSpawn.Remove(patient);
        }

        public void SubscribeToObserverPatient(IObserverCaractersInMap instantiatePatientView)
        {
            _observersPatients.Add(instantiatePatientView);
        }

        public void UnsubscribeToObserverPatient(IObserverCaractersInMap instantiatePatientView)
        {
            _observersPatients.Remove(instantiatePatientView);
        }

        public void notifyObserverCreationPatients(PatientDataModel patientDataModel)
        {
            foreach (var patient in _observersPatients)
            {
                patient.InstantiatePrefab(patientDataModel);
            }
        }
    }

}
