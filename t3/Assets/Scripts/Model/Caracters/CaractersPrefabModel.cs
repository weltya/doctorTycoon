using System.Collections.Generic;
using UnityEngine;

namespace Model.Caracters
{
    public class CaractersPrefabModel

    {
        private static CaractersPrefabModel _instance;
        private List<GameObject> _patientsPrefabs;

        private CaractersPrefabModel()
        {

        }

        public static CaractersPrefabModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CaractersPrefabModel();
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