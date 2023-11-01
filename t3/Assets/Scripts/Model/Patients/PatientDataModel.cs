using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model.Patients
{
    public class PatientDataModel
    {

        private Transform _targetChair;
        public PatientDataModel()
        {
        }

        public void SetTargetChair(Transform targetPosition)
        {
            _targetChair = targetPosition;
        }

        public Transform GetTargetChair()
        {
            return _targetChair;
        }
    }
}

