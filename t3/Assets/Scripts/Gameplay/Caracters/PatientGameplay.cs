using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Linq;
using Scripts.Utils.Enum;
using UnityEngine.AI;
using Scripts.Models.Caracters;
using System.Threading.Tasks;
using Scripts.Managers.Caracters;


namespace Scripts.Gameplay.Caracters
{
    public class PatientGameplay : MonoBehaviour
    {
        [SerializeField] public string Name;

        [SerializeField] private GameObject _assignedDoctors;
        [SerializeField] private int _assignedDoctorId;

        [SerializeField] private Room _currentRoom;
        [SerializeField] private Room _nextRoom;
        
        [SerializeField] private bool _isMoving;

        private int place;
        private Transform _targetPos;
        private NavMeshAgent _navMeshAgent;
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        bool didNurse=false;
        


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
        public void SetDestination(Transform destination, Room room)
        {
            if (!_navMeshAgent) _navMeshAgent = GetComponent<NavMeshAgent>();

            _isMoving = true;
            _nextRoom = room;
            _targetPos = destination;

            _navMeshAgent.SetDestination(destination.position);
        }

        public void MovePatientToReception(ReceptionRoomData room)
        {
            SetDestination(room.point, room);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomNurse(this);
        }

        public void MovePatientToWaitingRoom(WaitingRoomData room,int i)
        {          
            place=i; 
            SetDestination(room.ListPoint.ElementAt(i).Waypoint, room);        
        }

        public void MovePatientToDoctor(DoctorRoomData room)
        {
            SetDestination(room.point, room);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomDoctor(this);
        }
        public void MovePatientToNurse(NurseRoomData NurseRoom)
        {

            SetDestination(NurseRoom.point, NurseRoom);
            didNurse=true;
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomDoctor(this);
        }
        public void LibereRoom(){
            
            if(_currentRoom is DoctorRoomData){
                ((DoctorRoomData)_currentRoom).available=true;
            }
            else if(_currentRoom is WaitingRoomData){
                ((WaitingRoomData)_currentRoom).ListPoint[place].IsAvailable=true;;
            }else if(_currentRoom is NurseRoomData){
                ((NurseRoomData)_currentRoom).available=true;
            }else if(_currentRoom is ReceptionRoomData){
                ((ReceptionRoomData)_currentRoom).available=true;
            } 
            
        }
    }
}