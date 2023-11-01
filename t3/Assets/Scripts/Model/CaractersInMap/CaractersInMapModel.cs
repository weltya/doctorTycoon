using Model.Patients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaractersInMapModel
{
    private static CaractersInMapModel _instance;

    private HashSet<PatientDataModel> _patientsWaitingRoom;
    private List<IObserverCaractersInMap> _observersPatients;
    private CaractersInMapModel()
    {
        _patientsWaitingRoom = new HashSet<PatientDataModel>();
        _observersPatients = new List<IObserverCaractersInMap>();
    }

    public static CaractersInMapModel GetIntance()
    {
        if (_instance == null)
        {
            _instance = new CaractersInMapModel();
        }
        return _instance;
    }

    public HashSet<PatientDataModel> GetPatientsWaitingRoom()
    {
        return _patientsWaitingRoom;
    }

    public void AddPatientToWaitingRoom(PatientDataModel patient)
    {
        _patientsWaitingRoom.Add(patient);
        notifyObserverCreationPatients(patient);
    }

    public void RemovePatientWaitingRoom(PatientDataModel patient)
    {
        _patientsWaitingRoom.Remove(patient);
    }

    public void SubscribeToObserverPatient(IObserverCaractersInMap instantiatePatientView)
    {
        _observersPatients.Add(instantiatePatientView);
    }
    
    public void UnsubscribeToObserverPatient(IObserverCaractersInMap instantiatePatientView)
    {
        _observersPatients.Remove(instantiatePatientView);
    }

    public void notifyObserverCreationPatients(PatientDataModel patientDataModel)
    {
        foreach(var patient in _observersPatients)
        {
            patient.InstantiatePrefab(patientDataModel);
        }
    }
}
