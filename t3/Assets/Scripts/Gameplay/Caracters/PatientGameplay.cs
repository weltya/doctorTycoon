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

        private QueueManager _queueManager;
        private GameObject _goQueueManager;
        private UIScoreManager _uiScoreManager;
        private GameObject _goUiScoreManager;

        private Transform _targetPos;
        private NavMeshAgent _navMeshAgent;
        private ReceptionRoomData _roomReception;
        private WaitingRoomData _roomWaitingNurse;
        private PointData _roomWaitingNursePointdata;
        private NurseRoomData _roomNurse;
        private WaitingRoomData _roomWaitingDoctor;
        private PointData _roomWaitingDoctorPointdata;
        private DoctorRoomData _doctorRoomData;

        private bool end=false;
        private Vector3 _vector;
        private Transform _position;
        private float _maxSpawnZ = 33f;
        private float _minSpawnZ = 27f;
        private float _despawnX = 2f;


        private void Start()
        {
            _goQueueManager = GameObject.Find("QueueManager");
            _queueManager = _goQueueManager.GetComponent<QueueManager>();
            _goUiScoreManager = GameObject.Find("ScorePanel");
            if ( _goUiScoreManager == null )
            {
                Destroy(this.gameObject);
            }
            _uiScoreManager = _goUiScoreManager.GetComponent<UIScoreManager>();
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
                    StartCoroutine(WaitAndContinue(1f));
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

            if(end){
                _uiScoreManager.addMoney(200);
                Destroy(this.gameObject);
            }
            else if (_typeOfCurrentRoom == EnumRoom.Reception)
            {
                _roomReception.available = true;
                _queueManager.AddPatientInWaitingQueueNurse(this);
                _queueManager.CheckOrWaitToReception();
            } else if (_typeOfCurrentRoom == EnumRoom.WaitingRoom && _roomNurse == null)
            {
                    _roomWaitingNursePointdata.IsAvailable = true;
                    _queueManager.AddPatientInNurseQueue(this);
                    _queueManager.CheckOrWaitToWaitingNurseRoom();
                
            } else if (_typeOfCurrentRoom == EnumRoom.NurseRoom)
            {
                _roomNurse.available = true;
                _queueManager.AddPatientInWaitingQueueDoctor(this);
                _queueManager.CheckOrWaitToNurseRoom();
            } else if (_typeOfCurrentRoom == EnumRoom.WaitingRoom && _roomNurse != null)
            {
                _roomWaitingDoctorPointdata.IsAvailable = true;
                _queueManager.AddPatientInDoctorQueue(this);
                _queueManager.CheckOrWaitToWaitingDoctorRoom();
            } else if (_typeOfnextRoom == EnumRoom.DoctorRoom)
            {
                _doctorRoomData.available = true;
                _queueManager.CheckOrWaitToDoctorRoom();
                _queueManager.AddPatientToRemoveQueue(this);

            }
        }

        public void MovePatientToReception(ReceptionRoomData room)
        {
            
            _roomReception = room;
            _typeOfnextRoom = room.TypeRoom;
            int exp_sub= _roomReception.exp_sub();
            int grs = _roomReception.guerison();
            SetDestination(room.point);
            _uiScoreManager.setExp(exp_sub);
            _uiScoreManager.setGuerison(grs);
          

        }

        public void MovePatientToWaitingNurse(WaitingRoomData waitingRoom, PointData pointData)
        {  _roomWaitingNurse = waitingRoom;
            _typeOfnextRoom = waitingRoom.typeRoom;
            _roomWaitingNursePointdata = pointData;

            int exp_sub= _roomWaitingNurse.exp_sub();
            int grs= _roomWaitingNurse.guerison();
          
            SetDestination(pointData.Waypoint);
           _uiScoreManager.setExp(exp_sub);
            _uiScoreManager.setGuerison(grs);
        }

        public void MovePatientToNurse(NurseRoomData room)
        {
           
            _roomNurse = room;
            _typeOfnextRoom = room.typeRoom; 
            int exp_sub= _roomNurse.exp_sub();
            int grs= _roomNurse.guerison();
            SetDestination(room.point);
          _uiScoreManager.setExp(exp_sub);
            _uiScoreManager.setGuerison(grs);
        }

        public void MovePatientToWaitingDoctor(WaitingRoomData waitingRoom, PointData pointData)
        {
           
            _roomWaitingDoctor = waitingRoom;
            _typeOfnextRoom = waitingRoom.typeRoom;
            _roomWaitingDoctorPointdata = pointData; 
            int exp_sub= _roomWaitingDoctor.exp_sub();
            int grs= _roomWaitingDoctor.guerison();
        
            SetDestination(pointData.Waypoint);
            _uiScoreManager.setExp(exp_sub);
            _uiScoreManager.setGuerison(grs);
        }

        public void MovePatientToDoctor(DoctorRoomData room)
        {
            

            _doctorRoomData = room;
            _typeOfnextRoom = room.typeRoom;
            SetDestination(room.point); 
            int exp_sub=  _doctorRoomData.exp_sub();
            int grs=  _doctorRoomData.guerison();
          _uiScoreManager.setExp(exp_sub);
            _uiScoreManager.setGuerison(grs);
        }

        public void MoveToRemovePoint()
        {
            float despawnZ = Random.Range(_minSpawnZ, _maxSpawnZ);
            GameObject dynamicObject = new GameObject("DynamicObject");
            Transform _position = dynamicObject.transform;
            _vector=new Vector3(_despawnX,0,despawnZ);
            _position.position=_vector;
            SetDestination(_position);
            end=true;
        }
    }
}