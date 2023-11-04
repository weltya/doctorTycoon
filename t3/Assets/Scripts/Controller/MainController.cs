using System.Collections;
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
        public void MovePatientToRoomNurse()
        {
            _patientController.MovePatientToRoomNurse();
        }
        public void MovePatientToWaitingRoomDoctor()
        {
            _patientController.MovePatientToWaitingRoomDoctor();
        }
        public void MovePatientToDoctorRoom()
        {
            _patientController.MovePatientToDoctorRoom();
        }
        public void MovePatientToFinish()
        {
            _patientController.MovePatientToFinish();
        }

        public IEnumerator MovePatientsSequence()
        {
            MovePatientToReception();

            yield return new WaitForSeconds(6f);

            MovePatientToWaitingRoomNurse();

            yield return new WaitForSeconds(8f);

            MovePatientToRoomNurse();

            yield return new WaitForSeconds(14f);

            MovePatientToWaitingRoomDoctor();

            yield return new WaitForSeconds(14f);

            MovePatientToDoctorRoom();

            yield return new WaitForSeconds(14f);

            MovePatientToFinish();
        }
    }
}

