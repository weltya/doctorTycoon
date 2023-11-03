using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controller.PatientControllers;
using View.Caracters.Patients;
using View.Caracters;

namespace Controller
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] InstantiatePatientView _instantiatePatientView;
        [SerializeField] PatientNavMeshView _patientNavMeshView;

        private PatientController _patientController;

        private void Start()
        {
            _patientController = new PatientController(_instantiatePatientView, _patientNavMeshView);

            
            for (int i = 0; i < 3; i++)
            {
                CreatePatient();
                MoveToWaitingRoomWithDelay(1.0f);
                MoveToReceptionWithDelay(10.0f);
                MoveToWaitingRoomWithDelay(20.0f);
            }

            StartCoroutine(waiter(2f));

            //AssignWaypointsToPatientsGoingWaitingRoom();
        }

        IEnumerator waiter(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
        public void CreatePatient()
        {
            _patientController.CreatePatient();
        }

        public void AssignWaypointsToPatientsGoingWaitingRoom()
        {
            _patientController.AssignWaypointsToPatientsGoingWaitingRoom();
        }

        public void AssignWaypointsToPatientsGoingReception() {
            _patientController.AssignWaypointsToPatientsGoingReception();
        }

        public void MoveToWaitingRoomWithDelay(float delay) {
            StartCoroutine(MoveToWaitingRoomCoroutine(delay));
        }

        private IEnumerator MoveToWaitingRoomCoroutine(float delay) {
            yield return new WaitForSeconds(delay);
            AssignWaypointsToPatientsGoingWaitingRoom();
        }

        public void MoveToReceptionWithDelay(float delay) {
            StartCoroutine(MoveToReceptionCoroutine(delay));
        }

        private IEnumerator MoveToReceptionCoroutine(float delay) {
            yield return new WaitForSeconds(delay);
            AssignWaypointsToPatientsGoingReception();
        }
    }
}

