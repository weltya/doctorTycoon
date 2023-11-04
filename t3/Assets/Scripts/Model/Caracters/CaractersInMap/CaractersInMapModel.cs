using System;
using System.Collections.Generic;

using Model.Caracters.Patients;
using Model.Caracters.CaractersInMap;
using Model.Utils;

namespace Model.Characters.CaractersInMap
{
    public class CaractersInMapModel
    {
        private static CaractersInMapModel _instance;
        private readonly Dictionary<PatientState, HashSet<PatientDataModel>> _patientsByState;
        private readonly List<IObserverCaractersInMap> _observersPatients;

        private CaractersInMapModel()
        {
            _patientsByState = new Dictionary<PatientState, HashSet<PatientDataModel>>();
            foreach (PatientState state in Enum.GetValues(typeof(PatientState)))
            {
                _patientsByState[state] = new HashSet<PatientDataModel>();
            }
            _observersPatients = new List<IObserverCaractersInMap>();
        }

        public static CaractersInMapModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CaractersInMapModel();
            }
            return _instance;
        }

        public HashSet<PatientDataModel> GetPatientsByStateFromList(PatientState state)
        {
            return _patientsByState[state];
        }

        public void AddPatientInList(PatientState state, PatientDataModel patient)
        {
            if (_patientsByState[state].Add(patient) && state == PatientState.Spawn)
            {
                NotifyObserverCreationPatients(patient);
            }
        }

        public void RemovePatientFromList(PatientState state, PatientDataModel patient)
        {
            _patientsByState[state].Remove(patient);
        }

        public void SubscribeToObserverPatient(IObserverCaractersInMap observer)
        {
            _observersPatients.Add(observer);
        }

        public void Unsubscribe(IObserverCaractersInMap observer)
        {
            _observersPatients.Remove(observer);
        }

        private void NotifyObserverCreationPatients(PatientDataModel patientDataModel)
        {
            foreach (var observer in _observersPatients)
            {
                observer.InstantiatePrefab(patientDataModel);
            }
        }
    }
}