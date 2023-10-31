using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Model.Patient;


namespace Controller.SpawnController
{
    public class SpawnManager : MonoBehaviour
    {

        [SerializeField] private List<Patient> _patients = new List<Patient>();

        private Vector3 _patientPosition = new Vector3(-2, 0, 0);
        private Quaternion _patientRotation = Quaternion.Euler(0, 90, 0);
        private int _maxRangeSpawnZ = 7;
        private int _minRangeSpawnZ = -2;
        private int _nbrPatient;



        // Start is called before the first frame update
        void Start()
        {
            _nbrPatient = _patients.Count;
            for (int i = 0; i < 3; i++)
            {
                SpawnPatient();
            }
        }

        //Instantiate patient prefab
        public void SpawnPatient()
        {
            //random z point used for spawn prefab between min and max
            int spawnZ = Random.Range(_minRangeSpawnZ, _maxRangeSpawnZ);
            _patientPosition.z = spawnZ;

            //random int in _patients list count
            int nbrPatient = Random.Range(0, _nbrPatient);
            Instantiate(_patients[nbrPatient], _patientPosition, _patientRotation);
        }
    }
}

