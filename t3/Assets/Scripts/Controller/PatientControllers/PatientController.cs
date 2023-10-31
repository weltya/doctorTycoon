using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Model.Patients;
using Model.Waypoints;
using System;

namespace Controller.PatientControllers
{
    public class PatientController : MonoBehaviour
    {
        [SerializeField] PatientModel _patientModel;
        [SerializeField] WaitingRoomWayointModel _waitingRoomWayointModel;

        public void GoInTheWaitingRoom()
        {
            Transform chairPosition = _waitingRoomWayointModel.RequestChair();
            _patientModel.GoToSeatInWaitinRoom(chairPosition);
        }

    }
}

