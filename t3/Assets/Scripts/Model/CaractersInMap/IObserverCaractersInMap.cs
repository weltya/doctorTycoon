using Model.Patients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserverCaractersInMap
{
    //oldname : IObserverPatient
    public void InstantiatePrefab(PatientDataModel patientDataModel);
}
