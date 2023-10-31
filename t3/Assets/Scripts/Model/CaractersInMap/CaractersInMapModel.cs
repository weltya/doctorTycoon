using Model.Patients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model.CaractersInMap
{
    public class CaractersInMapModel : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _patientsInMap;

        private void Start()
        {
            _patientsInMap = new List<GameObject>();
        }

        public void AddPatient(GameObject patient)
        {
            _patientsInMap.Add(patient);
        }

        public void RemovePatient(GameObject patient)
        {
            _patientsInMap.Remove(patient);
        }
    }
}

