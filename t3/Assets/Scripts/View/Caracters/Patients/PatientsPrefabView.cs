using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace View.Caracters.Patients
{
    public class PatientsPrefabView : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _patientPrefabs = new List<GameObject>();

        public List<GameObject> GetPatientPrefabs()
        {
            return _patientPrefabs;
        }
    }
}

