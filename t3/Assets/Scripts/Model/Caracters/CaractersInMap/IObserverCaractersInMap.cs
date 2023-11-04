using Model.Caracters.Patients;

namespace Model.Caracters.CaractersInMap
{
    public interface IObserverCaractersInMap
    {
        //oldname : IObserverPatient
        public void InstantiatePrefab(PatientDataModel patientDataModel);
    }
}