using Model.Patients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaractersInMapModel
{
    private static CaractersInMapModel _instance;

    private HashSet<PatientDataModel> _patientsWaitingRoom;
    private List<IObserverPatient> _observersPatients;
    private CaractersInMapModel()
    {
        _patientsWaitingRoom = new HashSet<PatientDataModel>();
        _observersPatients = new List<IObserverPatient>();
    }

    public static CaractersInMapModel GetIntance()
    {
        if (_instance == null)
        {
            _instance = new CaractersInMapModel();
        }
        return _instance;
    }

    public void AddPatientToWaitingRoom(PatientDataModel patient)
    {
        _patientsWaitingRoom.Add(patient);
        notifyObserverCreationPatients();
    }

    public void RemovePatientWaitingRoom(PatientDataModel patient)
    {
        _patientsWaitingRoom.Remove(patient);
    }

    public void SubscribeToObserverPatient(IObserverPatient instantiatePatientView)
    {
        _observersPatients.Add(instantiatePatientView);
    }
    
    public void UnsubscribeToObserverPatient(IObserverPatient instantiatePatientView)
    {
        _observersPatients.Remove(instantiatePatientView);
    }

    public void notifyObserverCreationPatients()
    {
        foreach(var patient in _observersPatients)
        {
            patient.InstantiatePrefab();
        }
    }
}
