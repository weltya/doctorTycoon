using Scripts.Managers.Caracters;
using Scripts.Utils.Enum;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Gameplay.Caracters
{
    public class PatientGameplay : MonoBehaviour
    {
        [SerializeField] private EnumRoom _typeOfCurrentRoom;
        [SerializeField] private EnumRoom _typeOfnextRoom;
        
        [SerializeField] private bool _isMoving;

        private Transform _targetPos;
        private NavMeshAgent _navMeshAgent;
        private ReceptionRoomData _roomReception;
        private WaitingRoomData _roomWaitingNurse;
        private PointData _roomWaitingNursePointdata;
        private NurseRoomData _roomNurse;
        private WaitingRoomData _roomWaitingDoctor;
        private PointData _roomWaitingDoctorPointdata;
        private DoctorRoomData _doctorRoomData;


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
                    _typeOfCurrentRoom = _typeOfnextRoom;
                    _isMoving = false;
                    StartCoroutine(WaitAndContinue(5f));
                }
            }
        }
        private void SetDestination(Transform destination)
        {
            if (!_navMeshAgent) _navMeshAgent = GetComponent<NavMeshAgent>();

            _isMoving = true;
            _targetPos = destination;

            _navMeshAgent.SetDestination(destination.position);
        }

        private IEnumerator WaitAndContinue(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            if (_typeOfCurrentRoom == EnumRoom.Reception)
            {
                _roomReception.available = true;
                QueueManager.GetInstance().AddPatientInWaitingQueueNurse(this);
                QueueManager.GetInstance().CheckOrWaitToReception();
            } else if (_typeOfCurrentRoom == EnumRoom.WaitingRoom && _roomNurse == null)
            {
                    _roomWaitingNursePointdata.IsAvailable = true;
                    QueueManager.GetInstance().AddPatientInNurseQueue(this);
                    QueueManager.GetInstance().CheckOrWaitToWaitingNurseRoom();
                
            } else if (_typeOfCurrentRoom == EnumRoom.NurseRoom)
            {
                _roomNurse.available = true;
                QueueManager.GetInstance().AddPatientInWaitingQueueDoctor(this);
                QueueManager.GetInstance().CheckOrWaitToNurseRoom();
            } else if (_typeOfCurrentRoom == EnumRoom.WaitingRoom && _roomNurse != null)
            {
                _roomWaitingDoctorPointdata.IsAvailable = true;
                QueueManager.GetInstance().AddPatientInDoctorQueue(this);
                QueueManager.GetInstance().CheckOrWaitToWaitingDoctorRoom();
            } else if (_typeOfnextRoom == EnumRoom.DoctorRoom)
            {
                _doctorRoomData.available = true;
                QueueManager.GetInstance().CheckOrWaitToDoctorRoom();
                Destroy(this.gameObject);
            }

        }

        public void MovePatientToReception(ReceptionRoomData room)
        {
            _roomReception = room;
            _typeOfnextRoom = room.TypeRoom;
            SetDestination(room.point);
        }

        public void MovePatientToWaitingNurse(WaitingRoomData waitingRoom, PointData pointData)
        {
            _roomWaitingNurse = waitingRoom;
            _typeOfnextRoom = waitingRoom.typeRoom;
            _roomWaitingNursePointdata = pointData;
            SetDestination(pointData.Waypoint);
        }

        public void MovePatientToNurse(NurseRoomData room)
        {
            _roomNurse = room;
            _typeOfnextRoom = room.typeRoom;
            SetDestination(room.point);
        }

        public void MovePatientToWaitingDoctor(WaitingRoomData waitingRoom, PointData pointData)
        {
            _roomWaitingDoctor = waitingRoom;
            _typeOfnextRoom = waitingRoom.typeRoom;
            _roomWaitingDoctorPointdata = pointData;
            SetDestination(pointData.Waypoint);
        }

        public void MovePatientToDoctor(DoctorRoomData room)
        {
            _doctorRoomData = room;
            _typeOfnextRoom = room.typeRoom;
            SetDestination(room.point);
        }
        

    }
}