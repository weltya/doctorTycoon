using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Scripts.Managers.Caracters
{
    /**
     * @class PatientManager
     * @brief Manages the instantiation of patients in the game.
     */
    public class PatientManager : MonoBehaviour
    {
        [SerializeField] List<GameObject> _patientsPrefab = new List<GameObject>();

        [SerializeField] private Transform __patientsParents;

        private GameObject _goQueueManager;
        private QueueManager _queueManager;
        private Vector3 _position;
        private Quaternion _rotation = Quaternion.Euler(0, 90, 0);
        private float _maxSpawnZ = 33f;
        private float _minSpawnZ = 27f;
        private float _spawnX = 2f;

        /**
         * @brief Initializes the PatientManager by finding the QueueManager and starting patient instantiation.
         */
        private void Start()
        {
            _goQueueManager = GameObject.Find("QueueManager");
            _queueManager = _goQueueManager.GetComponent<QueueManager>();
            if (_patientsPrefab.Count <= 0)
            {
                Debug.LogError("_patientprefab is empty");
            }
            InvokeRepeating("InstantiatePatient",2f,3f);
        }

        /**
         * @brief Checks for user input to manually instantiate a patient.
         */
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                InstantiatePatient();
            }
        }
        

        /**
         * @brief Instantiates a patient and adds them to the reception queue.
         */
        private void InstantiatePatient()
        {
            GameObject go;
            System.Random rand=new System.Random();
            int randomindex=rand.Next(0,_patientsPrefab.Count);
            float spawnZ = UnityEngine.Random.Range(_minSpawnZ, _maxSpawnZ);
            _position = new Vector3(_spawnX, 0, spawnZ);

            go = Instantiate(_patientsPrefab[randomindex], _position, _rotation, __patientsParents);
            _queueManager.UpdateNbReceptionAddFix();
            _queueManager.AddPatientInSpawnQueue(go);
        }
    }
}

