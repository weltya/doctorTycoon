using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Utils.Enum;
using UnityEngine.AI;
using Scripts.Models.Caracters;

namespace Scripts.Gameplay.Caracters
{
    public class PatientGameplay : MonoBehaviour
    {
        #region[propriety]

        [SerializeField] public string Name;

        [SerializeField] private GameObject _assignedDoctors;
        [SerializeField] private int _assignedDoctorId;

        [SerializeField] private Room _currentRoom;
        [SerializeField] private Room _nextRoom;

        [SerializeField] private bool _isMoving;


        private Transform _targetPos;
        private NavMeshAgent _navMeshAgent;

        #endregion[propriety]

        #region[unity function]
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_isMoving)
            {
                if (Vector3.Distance(transform.position, _targetPos.position) < 0.5f)
                {
                    _currentRoom = _nextRoom;
                    _isMoving = false;
                }
            }
        }
        #endregion[unity function]

        #region[setter getter]

        #endregion[setter getter]

        #region[navmesh]
        public void SetDestination(Transform destination, Room room)
        {
            if (!_navMeshAgent) _navMeshAgent = GetComponent<NavMeshAgent>();

            _isMoving = true;
            _nextRoom = room;
            _targetPos = destination;

            _navMeshAgent.SetDestination(destination.position);
        }
        #endregion[navmesh]

        #region[send patient to]
        public void MovePatientToreception()
        {

        }
        #endregion[send patient to]

    }
}

