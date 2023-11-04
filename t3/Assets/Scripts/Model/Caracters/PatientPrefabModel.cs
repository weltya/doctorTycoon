using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model.Caracters
{
    public class PatientPrefabModel
    {
        private static PatientPrefabModel _instance;
        private List<GameObject> _patientsPrefabs;

        private PatientPrefabModel()
        {

        }

        public static PatientPrefabModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PatientPrefabModel();
            }
            return _instance;
        }

        public void SetPatientsPrefabs(List<GameObject> lists)
        {
            _patientsPrefabs = new List<GameObject>(lists);
        }

        public List<GameObject> GetPatientsPrefabs()
        {
            return _patientsPrefabs;
        }
    }
}


