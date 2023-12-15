using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

using Scripts.Utils.Enum;
using UnityEngine.AI;
using Scripts.Models.Caracters;
using Scripts.Managers.Caracters;


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
        public void MovePatientToReception(ReceptionRoomData room)
        {
            SetDestination(room.point, room);
            Thread.Sleep(4000);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomNurse(this);
        }
        #endregion[send patient to]

        #region [send patient to]
        public void MovePatientToWaitingRoom(WaitingRoomData room)
        {
            SetDestination(room.point, room);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomNurse(this);
        }
        #endregion[send patient to]
        public void MovePatientToDoctor(DoctorRoomData room)
        {
            SetDestination(room.point, room);
            Thread.Sleep(7000);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomDoctor(this);
        }
        public void MovePatientToNurse(NurseRoomData NurseRoom)
        {

            SetDestination(NurseRoom.point, NurseRoom);
            Thread.Sleep(5000);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomNurse(this);
        }
    }
}