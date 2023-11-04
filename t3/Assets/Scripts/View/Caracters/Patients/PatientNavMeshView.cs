using UnityEngine;
using UnityEngine.AI;

using Model.Caracters.Patients;

namespace View.Caracters
{
    public class PatientNavMeshView : MonoBehaviour, IObserverPatient
    {
        private NavMeshAgent _navMeshAgent;
        private Transform _targetPosition;

        private void Start()
        {
            if (gameObject.name != "PatientNavMeshView")
            {
                _navMeshAgent = GetComponent<NavMeshAgent>();
                if (_navMeshAgent == null)
                {
                    Debug.LogError("NavMeshAgent not found on the GameObject.");
                }
            }       
        }
        public void notifyNewTargetChair(Transform targetPositon, GameObject gameObject)
        {
            if (gameObject.name != "PatientNavMeshView")
            {
                _targetPosition = targetPositon;
                _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

                if (_targetPosition != null && _navMeshAgent != null)
                {
                    _navMeshAgent.SetDestination(_targetPosition.position);
                }
            }
             
        }
    }
}