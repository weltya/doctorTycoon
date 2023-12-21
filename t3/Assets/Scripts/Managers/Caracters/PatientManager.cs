using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers.Caracters
{
    public class PatientManager : MonoBehaviour
    {
        [SerializeField] List<GameObject> _patientsPrefab = new List<GameObject>();

        [SerializeField] private Transform __patientsParents;

        private Vector3 _position;
        private Quaternion _rotation = Quaternion.Euler(0, 90, 0);
        private float _maxSpawnZ = 33f;
        private float _minSpawnZ = 27f;
        private float _spawnX = 2f;

        private void Start()
        {
            if (_patientsPrefab.Count <= 0)
            {
                Debug.LogError("_patientprefab is empty");
            }
            InvokeRepeating("InstantiatePatient",2f,3f);
        }
        

        private void InstantiatePatient()
        {
            GameObject go;

            float spawnZ = Random.Range(_minSpawnZ, _maxSpawnZ);
            _position = new Vector3(_spawnX, 0, spawnZ);

            go = Instantiate(_patientsPrefab[0], _position, _rotation, __patientsParents);
            QueueManager.GetInstance().AddPatientInSpawnQueue(go);
        }
    }
}

