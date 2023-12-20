using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Linq;
using Scripts.Utils.Enum;
using UnityEngine.AI;
using Scripts.Models.Caracters;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
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


        private Transform _targetPos;
        private NavMeshAgent _navMeshAgent;
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled  = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                    // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                    Application.DoEvents();
            }
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
            SetDestination(room.points.ElementAt(i).Key, room);
            wait(5000);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomNurse(this);
        }

        public void MovePatientToDoctor(DoctorRoomData room)
        {
            SetDestination(room.point, room);
            //Thread.Sleep(7000);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomDoctor(this);
        }
        public void MovePatientToNurse(NurseRoomData NurseRoom)
        {

            SetDestination(NurseRoom.point, NurseRoom);
            //Thread.Sleep(5000);
            QueueManager.GetInstance().CheckOrWaitToWaitingRoomNurse(this);
        }
    }
}