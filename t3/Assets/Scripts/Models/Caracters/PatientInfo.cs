using UnityEngine;

using Scripts.Utils.Enum;

namespace Scripts.Models.Caracters
{
    public class PatientInfo
    {
        private string _name;
        private int _age;
        private Gender _gender;
        private Disease _disease;
        private GameObject _patientPrefab;

        public PatientInfo(string name, int age, Gender gender, Disease disease, GameObject patientPrefab)
        {
            this._name = name;
            this._age = age;
            this._gender = gender;
            this._disease = disease;
            _patientPrefab = patientPrefab;
        }
    }
}

