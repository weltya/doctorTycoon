using Model.Patients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View.Caracters.Patients;

namespace Controller.PatientControllers
{
    public class PatientController
    {
        private PatientDataModel _patientDataModel;
        private CaractersInMapModel _caractersInMap;
        private InstantiatePatientView _instantiatePatientView;
        public PatientController(InstantiatePatientView instantiatePatientView)
        {
            _instantiatePatientView = instantiatePatientView;
            _caractersInMap = CaractersInMapModel.GetIntance();
            _caractersInMap.SubscribeToObserverPatient(_instantiatePatientView);
        }

        public void CreatePatient()
        {
            _patientDataModel = new PatientDataModel();
            _caractersInMap.AddPatientToWaitingRoom(_patientDataModel);
        }
    }
}

