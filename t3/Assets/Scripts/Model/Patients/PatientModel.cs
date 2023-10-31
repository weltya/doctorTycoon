using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Model.Patients
{
    public class PatientModel : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        private string _name = "patient1";
        private Transform _targetChair;
        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();

            if (_agent == null)
            {
                Debug.LogError("NavMeshAgent non trouvé sur le patient!");
            }
        }


        private void Update()
        {
            if (_targetChair != null && !_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                _agent.isStopped = true;
            }

        }

        public GameObject GetPrefab() 
        {
            return _prefab; 
        }

        public void GoToSeatInWaitinRoom(Transform chair)
        {
            Debug.Log("GoToSeat");
            _agent.SetDestination(chair.position);
        }

    }
}
