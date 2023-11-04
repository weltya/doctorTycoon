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
            }

            StartCoroutine(MovePatientsSequence());
        }

        IEnumerator waiter(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
        public void CreatePatient()
        {
            _patientController.CreatePatient();
        }

        public void MovePatientToReception()
        {
            _patientController.MovePatientToReception();
        }
        public void MovePatientToWaitingRoomNurse()
        {
            _patientController.MovePatientToWaitingRoomNurse();
        }

        public IEnumerator MovePatientsSequence()
        {
            MovePatientToReception();

            yield return new WaitForSeconds(6f);

            MovePatientToWaitingRoomNurse();

            yield return new WaitForSeconds(6f);

        }
    }
}

