using Scripts.Managers.Caracters;
using Scripts.Utils.Enum;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Scripts.UII;


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
            }
            else{

            }

        }

        public void MovePatientToReception(ReceptionRoomData room)
        {
            
            _roomReception = room;
            _typeOfnextRoom = room.TypeRoom;
            int exp_sub= _roomReception.exp_sub();
            int grs = _roomReception.guerison();
            SetDestination(room.point);
            score.setExp(exp_sub);
            score.setGuerison(grs);

        }

        public void MovePatientToWaitingNurse(WaitingRoomData waitingRoom, PointData pointData)
        {
            int exp_sub= _roomWaitingNurse.exp_sub();
            int grs= _roomWaitingNurse.guerison();
            _roomWaitingNurse = waitingRoom;
            _typeOfnextRoom = waitingRoom.typeRoom;
            _roomWaitingNursePointdata = pointData;
            SetDestination(pointData.Waypoint);
            score.setExp(exp_sub);
            score.setGuerison(grs);
        }

        public void MovePatientToNurse(NurseRoomData room)
        {
            int exp_sub= _roomNurse.exp_sub();
            int grs= _roomNurse.guerison();
            _roomNurse = room;
            _typeOfnextRoom = room.typeRoom;
            SetDestination(room.point);
             score.setExp(exp_sub);
            score.setGuerison(grs);
        }

        public void MovePatientToWaitingDoctor(WaitingRoomData waitingRoom, PointData pointData)
        {
            int exp_sub= _roomWaitingDoctor.exp_sub();
            int grs= _roomWaitingDoctor.guerison();
        
            _roomWaitingDoctor = waitingRoom;
            _typeOfnextRoom = waitingRoom.typeRoom;
            _roomWaitingDoctorPointdata = pointData;
            SetDestination(pointData.Waypoint);
             score.setExp(exp_sub);
            score.setGuerison(grs);
        }

        public void MovePatientToDoctor(DoctorRoomData room)
        {
             int exp_sub=  _doctorRoomData.exp_sub();
            int grs=  _doctorRoomData.guerison();

            _doctorRoomData = room;
            _typeOfnextRoom = room.typeRoom;
            SetDestination(room.point);
            score.setExp(exp_sub);
            score.setGuerison(grs);
        }
        public void MoveToRemovePoint()
        {
            //SetDestination();
        }

    }
}