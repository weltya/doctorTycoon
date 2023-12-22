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

        private SavePatientAndHisWaypoint _savePatientAndHisWaypoint = new SavePatientAndHisWaypoint();


        private void Start()
        {
            _goQueueManager = GameObject.Find("QueueManager");
            _queueManager = _goQueueManager.GetComponent<QueueManager>();
            _goUiScoreManager = GameObject.Find("ScorePanel");
            if ( _goUiScoreManager == null )
            {
                Debug.LogError("_goUiScoreManager = null");
            }
            _uiScoreManager = _goUiScoreManager.GetComponent<UIScoreManager>();

            if ( _uiScoreManager == null )
            {
                Debug.LogError("_uiScoreManager = null");
            }
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _savePatientAndHisWaypoint.PatientGameplay = this;
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
                _queueManager.CheckOrWaitToDoctorRoom();
                Destroy(this.gameObject);
            }
            else if (_typeOfCurrentRoom == EnumRoom.Reception)
            {  
                _queueManager.UpdateNbReceptionRemoveFix();
                _queueManager.AddPatientInWaitingQueueNurse(_savePatientAndHisWaypoint);
                _queueManager.CheckOrWaitToReception();
            } else if (_typeOfCurrentRoom == EnumRoom.WaitingRoom && _roomNurse == null)
            {
                    _queueManager.AddPatientInNurseQueue(_savePatientAndHisWaypoint);
                    _queueManager.CheckOrWaitToWaitingNurseRoom();
                
            } else if (_typeOfCurrentRoom == EnumRoom.NurseRoom)
            {
                _queueManager.AddPatientInWaitingQueueDoctor(_savePatientAndHisWaypoint);
                _queueManager.CheckOrWaitToNurseRoom();
            } else if (_typeOfCurrentRoom == EnumRoom.WaitingRoom && _roomNurse != null)
            {
                _queueManager.AddPatientInDoctorQueue(_savePatientAndHisWaypoint);
                _queueManager.CheckOrWaitToWaitingDoctorRoom();
            } else if (_typeOfnextRoom == EnumRoom.DoctorRoom)
            {
                _queueManager.CheckOrWaitToDoctorRoom();
                _queueManager.AddPatientToRemoveQueue(_savePatientAndHisWaypoint);

            }
        }

        public void MovePatientToReception(ReceptionRoomData room)
        {
            _roomReception = room;
            _typeOfnextRoom = room.TypeRoom;

            _savePatientAndHisWaypoint.ReceptionRoomData = room;

            SetDestination(room.Point);
        }

        public void MovePatientToWaitingNurse(WaitingRoomData waitingRoom, PointData pointData)
        {  _roomWaitingNurse = waitingRoom;
            _typeOfnextRoom = waitingRoom.TypeRoom;
            _roomWaitingNursePointdata = pointData;

            _savePatientAndHisWaypoint.WaitingRoomData = waitingRoom;
            _savePatientAndHisWaypoint.PointDataWaitingNurse = pointData;

            float exp_sub = _roomWaitingNurse.ExpSubjective;
            float grs = _roomWaitingNurse.Health;
          
            _uiScoreManager.setExp(exp_sub);
            _uiScoreManager.setGuerison(grs);

            SetDestination(pointData.Waypoint);
        }

        public void MovePatientToNurse(NurseRoomData room)
        {
           
            _roomNurse = room;
            _typeOfnextRoom = room.TypeRoom;

            _savePatientAndHisWaypoint.NurseRoomData = room;

            float exp_sub= _roomNurse.ExpSubjective;
            float grs= _roomNurse.Health;

            _uiScoreManager.setExp(exp_sub);
            _uiScoreManager.setGuerison(grs);

            SetDestination(room.Point);
        }

        public void MovePatientToWaitingDoctor(WaitingRoomData waitingRoom, PointData pointData)
        {
           
            _roomWaitingDoctor = waitingRoom;
            _typeOfnextRoom = waitingRoom.TypeRoom;
            _roomWaitingDoctorPointdata = pointData;

            _savePatientAndHisWaypoint.WaitingRoomData2 = waitingRoom;
            _savePatientAndHisWaypoint.PointDataWaitingNurse2 = pointData;

            float exp_sub= _roomWaitingDoctor.ExpSubjective;
            float grs= _roomWaitingDoctor.Health;
        
            _uiScoreManager.setExp(exp_sub);
            _uiScoreManager.setGuerison(grs);
            SetDestination(pointData.Waypoint);
        }

        public void MovePatientToDoctor(DoctorRoomData room)
        {
            _doctorRoomData = room;
            _typeOfnextRoom = room.TypeRoom;

            _savePatientAndHisWaypoint.DoctorRoomData = room;

            float exp_sub=  _doctorRoomData.ExpSubjective;
            float grs=  _doctorRoomData.Health;

            _uiScoreManager.setExp(exp_sub);
            _uiScoreManager.setGuerison(grs);
            SetDestination(room.Point);
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