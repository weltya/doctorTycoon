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

        private WaitingRoomData wtgroom;
        private int place =-1;
        private Transform _targetPos;
        private NavMeshAgent _navMeshAgent;


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
                    StartCoroutine(WaitAndContinue(5f));
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
        public IEnumerator sendPatienttoReception(ReceptionRoomData room){
            SetDestination(room.point, room);
            yield return new WaitForSeconds(5f);

            QueueManager.GetInstance().LibereRoom(room);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomNurse(this);
        }
        public IEnumerator sendPatienttoWaitingRoom(WaitingRoomData room,int i){
            wtgroom=room;
            place=i;
            SetDestination(room.ListPoint.ElementAt(i).Waypoint, room);
            yield return new WaitForSeconds(5f);
        }

        /*public void MovePatientToReception(ReceptionRoomData room)
        {
            SetDestination(room.point, room);
            
            QueueManager.GetInstance().LibereRoom(room);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomNurse(this);
        }*/
        public void MovePatientToReception(ReceptionRoomData room){
            StartCoroutine(sendPatienttoReception(room));
        }



        /*public void MovePatientToWaitingRoom(WaitingRoomData room,int i)
        {   
            wtgroom=room;
            place=i;
            SetDestination(room.ListPoint.ElementAt(i).Waypoint, room);
        }*/
        public void MovePatientToWaitingRoom(WaitingRoomData room,int i){
            StartCoroutine(sendPatienttoWaitingRoom(room,i));
        }

        public void MovePatientToDoctor(DoctorRoomData room)
        {
            if(place!=-1){
                place=-1;
                QueueManager.GetInstance().LibereWaitingRoom(wtgroom,place);
            }
            SetDestination(room.point, room);
            QueueManager.GetInstance().LibereRoom(room);            
        }
        public void MovePatientToNurse(NurseRoomData NurseRoom)
        {
            if(place!=-1){
                place=-1;
                QueueManager.GetInstance().LibereWaitingRoom(wtgroom,place);
            }
            SetDestination(NurseRoom.point, NurseRoom);
            QueueManager.GetInstance().LibereRoom(NurseRoom);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomDoctor(this);
        }
        private IEnumerator WaitAndContinue(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }
        
    }
}