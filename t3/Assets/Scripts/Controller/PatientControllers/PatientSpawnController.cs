using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Model.Patients;
using Model.CaractersInMap;


namespace Controller.PatientControllers
{
    public class PatientSpawnController : MonoBehaviour
    {
        [SerializeField] private PatientModel _patientModel;
        [SerializeField] private CaractersInMapModel _caractersInMapModel;

        private Vector3 _patientPosition = new Vector3(0, 1, 0);
        private Quaternion _patientRotation = Quaternion.Euler(0, 90, 0);
        private int _maxRangeSpawnZ = 7;
        private int _minRangeSpawnZ = -2;
        private int _nbrPatient;


        //Instantiate patient prefab
        public void SpawnPatient()
        {
            //random z point used for spawn prefab between min and max
            int spawnZ = UnityEngine.Random.Range(_minRangeSpawnZ, _maxRangeSpawnZ);
            _patientPosition.z = spawnZ;
            //random int in _patients list count
            int nbrPatient = UnityEngine.Random.Range(0, _nbrPatient);
            GameObject spawnedPatient = Instantiate(_patientModel.GetPrefab(), _patientPosition, _patientRotation);

            _caractersInMapModel.AddPatient(spawnedPatient);
        }
    }
}

