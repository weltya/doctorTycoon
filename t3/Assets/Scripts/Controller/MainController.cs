using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controller.PatientControllers;

namespace Controller
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] private PatientController _patientController;
        [SerializeField] private PatientSpawnController _PatientSpawnController;

        private void Start()
        {
            /*
            for (int i = 0; i < 3; i++)
            {
                CreatePatient();
            }
            */
            //StartCoroutine(waiter(2f));

            GoingInWaitingRoom();
        }

        IEnumerator waiter(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
        public void CreatePatient()
        {
            _PatientSpawnController.SpawnPatient();
        }

        public void GoingInWaitingRoom()
        {
            _patientController.GoInTheWaitingRoom();
        }

    }




}

