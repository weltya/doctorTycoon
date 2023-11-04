using UnityEngine;

namespace Model.Caracters.Patients
{
    public interface IObserverPatient
    {
        public void notifyNewTargetChair(Transform targetPositon, GameObject gameObject);
    }
}