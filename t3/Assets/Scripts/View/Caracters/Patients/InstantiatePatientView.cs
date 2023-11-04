using Controller;
using Model.Patients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace View.Caracters.Patients
{
    public class InstantiatePatientView : MonoBehaviour, IObserverCaractersInMap
    {
        //[SerializeField] PatientsPrefabView _patientsPrefabView;
        [SerializeField] DataController _dataController;
        private Vector3 _position;
        private Quaternion _rotation = Quaternion.Euler(0, 90, 0);
        private float _maxSpawnZ = 6f;
        private float _minSpawnZ = -1f;
        private float _spawnX = 2f;

        public void InstantiatePrefab(PatientDataModel patientDataModel)
        {
            float spawnZ = Random.Range(_minSpawnZ, _maxSpawnZ);
            _position = new Vector3(_spawnX, 0, spawnZ);

            List<GameObject> prefabList = new List<GameObject>();
            prefabList = _dataController.GetPatientPrefabs();
            GameObject go = Instantiate(prefabList[0], _position, _rotation);
            patientDataModel.SetGameobject(go);
        }
    }
}

