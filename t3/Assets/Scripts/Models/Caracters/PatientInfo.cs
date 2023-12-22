using UnityEngine;

using Scripts.Utils.Enum;

namespace Scripts.Models.Caracters
{   
    /**
     * @class PatientInfo
     * @brief Represents information about a patient, including name, age, gender, disease, and associated prefab.
     */
    public class PatientInfo
    {
        private string _name;
        private int _age;
        private Gender _gender;
        private GameObject _patientPrefab;

        /**
         * @brief Initializes a new instance of the PatientInfo class.
         * @param {string} name - The name of the patient.
         * @param {int} age - The age of the patient.
         * @param {Gender} gender - The gender of the patient.
         * @param {GameObject} patientPrefab - The prefab associated with the patient.
         */
        public PatientInfo(string name, int age, Gender gender, GameObject patientPrefab)
        {
            this._name = name;
            this._age = age;
            this._gender = gender;
            _patientPrefab = patientPrefab;
        }
    }
}

