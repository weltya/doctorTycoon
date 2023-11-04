using System.Collections.Generic;
using UnityEngine;

namespace Model.Caracters.Patients
{
    public class PatientDataModel
    {
        private GameObject _gameobject;
        private Transform _targetChair;
        public string name = "patient1";
        private List<IObserverPatient> _observersPatient = new();
        public PatientDataModel()
        {
        }

        public void SetGameobject(GameObject gameObject)
        {
            _gameobject = gameObject;
        }
        public GameObject GetGameobject()
        {
            return _gameobject;
        }
        public void SetTargetChair(Transform targetPosition)
        {
            _targetChair = targetPosition;
            notifyObserverPositionPatient();
        }

        public Transform GetTargetChair()
        {
            return _targetChair;
        }

        public void SubscribeToObserverPatient(IObserverPatient instantiatePatientView)
        {
            _observersPatient.Add(instantiatePatientView);
        }

        public void UnsubscribeToObserverPatient(IObserverPatient instantiatePatientView)
        {
            _observersPatient.Remove(instantiatePatientView);
        }

        public void notifyObserverPositionPatient()
        {
            foreach (var patient in _observersPatient)
            {
                patient.notifyNewTargetChair(_targetChair, _gameobject);
            }
        }
    }
}