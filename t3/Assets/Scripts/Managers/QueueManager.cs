using Scripts.Gameplay.Caracters;
using Scripts.Managers.BuilderManagers;
using Scripts.UII;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Managers.Caracters
{
    public class QueueManager : MonoBehaviour
    {
        private Queue<PatientGameplay> _waitingQueueReception = new Queue<PatientGameplay>();
        public int NbCaractersInReception = 0;
        private Queue<SavePatientAndHisWaypoint> _waitingQueueWaitingNurse = new Queue<SavePatientAndHisWaypoint>();
        private Queue<SavePatientAndHisWaypoint> _waitingQueueNurse = new Queue<SavePatientAndHisWaypoint>();
        private Queue<SavePatientAndHisWaypoint> _waitingQueueWaitingDoctor = new Queue<SavePatientAndHisWaypoint>();
        public int NbCaractersInWaiting = 0;
        private Queue<SavePatientAndHisWaypoint> _waitingQueueDoctor = new Queue<SavePatientAndHisWaypoint>();
        private Queue<SavePatientAndHisWaypoint> _waitingQueueRemove=new Queue<SavePatientAndHisWaypoint>();

        private List<ReceptionRoomData> ListReceptionRoom = new List<ReceptionRoomData>();
        public int NbCapacityWaiting = 0;
        private List<WaitingRoomData> ListWaitingRoom = new List<WaitingRoomData>();
        private List<DoctorRoomData> ListDoctor = new List<DoctorRoomData>();
        private List<NurseRoomData> ListNurse = new List<NurseRoomData>();
        [SerializeField] ObjectPlacer _objectPlacer;

        private UIScoreManager _uiScoreManager;
        private GameObject _goUiScoreManager;


        public struct WaitingRoomStruct
        {
            public WaitingRoomData waitingRoom;
            public PointData pointData;
        }

        private void Start()
        {
            _objectPlacer.onObjectPlaced += AddRoom;
            //avoid any type of blocking
            InvokeRepeating("CheckOrWaitToReception", 5f,1f);
            InvokeRepeating("CheckOrWaitToWaitingNurseRoom", 5f, 1f);
            InvokeRepeating("CheckOrWaitToWaitingDoctorRoom", 5f, 1f);
            InvokeRepeating("CheckOrWaitToNurseRoom", 5f, 1f);
            InvokeRepeating("CheckOrWaitToDoctorRoom", 5f, 1f);
            InvokeRepeating("CheckOrWaitToRemove", 5f, 1f);
        }
        private void Awake()
        {
            Debug.Log("instance queuemanager créer ");
            _goUiScoreManager = GameObject.Find("ScorePanel");
            _uiScoreManager = _goUiScoreManager.GetComponent<UIScoreManager>();
        }
        
        private ReceptionRoomData IsReceptionIsAvailable()
        {
            for (int i = 0; i < ListReceptionRoom.Count; i++)
            {
                if (ListReceptionRoom[i].IsAvailable)
                {
                    ListReceptionRoom[i].IsAvailable = false;
                    return ListReceptionRoom[i];
                }
            }
            return null;
        }
        private DoctorRoomData IsDoctorIsAvailable()
        {
            for (int i = 0; i < ListDoctor.Count; i++)
            {
                if (ListDoctor[i].IsAvailable)
                {
                    ListDoctor[i].IsAvailable = false;
                    return ListDoctor[i];
                }
            }
            return null;
        }
        private NurseRoomData IsNurseIsAvailable()
        {
            for (int i = 0; i < ListNurse.Count; i++)
            {
                if (ListNurse[i].IsAvailable)
                {
                    ListNurse[i].IsAvailable = false;
                    return ListNurse[i];
                }
            }
            return null;
        }
        private WaitingRoomStruct IsWaitingRoomAvailable()
        {
            WaitingRoomStruct waitingRoomStruct;
            foreach (var waitingRoomData in ListWaitingRoom)
            {
                if (waitingRoomData.Capacity >= waitingRoomData.MaxCapacity)
                {
                    break;
                }
                foreach (var pointData in waitingRoomData.ListPoint)
                {
                    if (pointData.IsAvailable)
                    {
                        waitingRoomStruct.waitingRoom = waitingRoomData;
                        waitingRoomStruct.pointData = pointData;
                        return waitingRoomStruct;
                    }
                }
            }
            waitingRoomStruct.waitingRoom = null;
            waitingRoomStruct.pointData = null;
            return waitingRoomStruct;
        }
            
     
        private void AddRoom(Room room)
        {
            if (room is DoctorRoomData)
            {
                ListDoctor.Add((DoctorRoomData)room);
                _uiScoreManager.UpdateNbRoomDoctor(ListDoctor.Count);
                CheckOrWaitToDoctorRoom();
            }
            else if (room is WaitingRoomData)
            {
                ListWaitingRoom.Add((WaitingRoomData)room);
                NbCapacityWaiting += ((WaitingRoomData)room).MaxCapacity;
                _uiScoreManager.UpdateNbRoomWaiting(NbCapacityWaiting);
                CheckOrWaitToWaitingNurseRoom();
                CheckOrWaitToWaitingDoctorRoom();
            }
            else if (room is NurseRoomData)
            {
                ListNurse.Add((NurseRoomData)room);
                _uiScoreManager.UpdateNbRoomNurse(ListNurse.Count);
                CheckOrWaitToNurseRoom();
            }
            else if (room is ReceptionRoomData)
            {
                ListReceptionRoom.Add((ReceptionRoomData)room);
                _uiScoreManager.UpdateNbRoomReception(ListReceptionRoom.Count);
                CheckOrWaitToReception();
            }
        }

        public void CheckOrWaitToReception()
        {
            if (_waitingQueueReception.Count <= 0)
            {
                return;
            }

            PatientGameplay patient = _waitingQueueReception.Peek();
            ReceptionRoomData wheretogo = IsReceptionIsAvailable();
            if (wheretogo)
            {
                wheretogo.IsAvailable = false;
                _waitingQueueReception.Dequeue();
                patient.MovePatientToReception(wheretogo);
            }
        }
        public void CheckOrWaitToWaitingNurseRoom()
        {
            if (_waitingQueueWaitingNurse.Count <= 0)
            {
                return;
            }

            SavePatientAndHisWaypoint patient = _waitingQueueWaitingNurse.Peek();
            WaitingRoomStruct whereToGo = IsWaitingRoomAvailable();
            if (whereToGo.waitingRoom != null)
            {
                patient.ReceptionRoomData.IsAvailable = true;

                whereToGo.pointData.IsAvailable = false;
                _waitingQueueWaitingNurse.Dequeue();
                _uiScoreManager.UpdateNbPatientWaiting(NbCaractersInWaiting);
                patient.PatientGameplay.MovePatientToWaitingNurse(whereToGo.waitingRoom, whereToGo.pointData);  
            }
        }

        public void CheckOrWaitToNurseRoom()
        {
            if (_waitingQueueNurse.Count <= 0)
            {
                return;
            }

            SavePatientAndHisWaypoint patient = _waitingQueueNurse.Peek();
            NurseRoomData whereToGo = IsNurseIsAvailable();

            if (whereToGo != null)
            {
                patient.PointDataWaitingNurse.IsAvailable = true;

                whereToGo.IsAvailable = false;
                _waitingQueueNurse.Dequeue();
                _uiScoreManager.UpdateNbPatientNurse(_waitingQueueNurse.Count + _waitingQueueWaitingNurse.Count);
                patient.PatientGameplay.MovePatientToNurse(whereToGo);
            }
        }

        public void CheckOrWaitToWaitingDoctorRoom()
        {
            if (_waitingQueueWaitingDoctor.Count <= 0)
            {
                return;
            }

            SavePatientAndHisWaypoint patient = _waitingQueueWaitingDoctor.Peek();
            WaitingRoomStruct whereToGo = IsWaitingRoomAvailable();

            if (whereToGo.waitingRoom != null)
            {
                patient.NurseRoomData.IsAvailable = true;

                whereToGo.pointData.IsAvailable = false;
                _waitingQueueWaitingDoctor.Dequeue();
                _uiScoreManager.UpdateNbPatientWaiting(NbCaractersInWaiting);
                patient.PatientGameplay.MovePatientToWaitingDoctor(whereToGo.waitingRoom, whereToGo.pointData);
            }
      
        }
        public void CheckOrWaitToDoctorRoom()
        {
            if (_waitingQueueDoctor.Count <= 0)
            {
                return;
            }

            SavePatientAndHisWaypoint patient = _waitingQueueDoctor.Peek();
            DoctorRoomData whereToGo = IsDoctorIsAvailable();

            if (whereToGo != null)
            {
                patient.PointDataWaitingNurse2.IsAvailable = true;

                whereToGo.IsAvailable = false;
                _waitingQueueDoctor.Dequeue();
                _uiScoreManager.UpdateNbPatientDoctor(_waitingQueueDoctor.Count);
                patient.PatientGameplay.MovePatientToDoctor(whereToGo);
            }
        }
        public void CheckOrWaitToRemove(){

            if(_waitingQueueRemove.Count<=0){
                return;
            }

            SavePatientAndHisWaypoint patient=_waitingQueueRemove.Peek();

            patient.DoctorRoomData.IsAvailable = true;
            patient.PatientGameplay.MoveToRemovePoint();
            _waitingQueueRemove.Dequeue();
            //_uiScoreManager.UpdateNbPatientDoctor(_waitingQueueDoctor.Count);
        }


        public void AddPatientInSpawnQueue(GameObject go)
        {
            PatientGameplay scriptPatientGameplay = go.GetComponent<PatientGameplay>();
            _waitingQueueReception.Enqueue(scriptPatientGameplay);
            _uiScoreManager.UpdateNbPatientReception(NbCaractersInReception);
            CheckOrWaitToReception();
        }

        public void AddPatientInWaitingQueueNurse(SavePatientAndHisWaypoint patient)
        {
            _waitingQueueWaitingNurse.Enqueue(patient);
            _uiScoreManager.UpdateNbPatientWaiting(NbCaractersInWaiting);
            CheckOrWaitToWaitingNurseRoom();
        }

        public void AddPatientInNurseQueue(SavePatientAndHisWaypoint patient)
        {
            _waitingQueueNurse.Enqueue(patient);
            _uiScoreManager.UpdateNbPatientNurse(_waitingQueueNurse.Count + _waitingQueueWaitingNurse.Count);
            CheckOrWaitToNurseRoom(); 
        }

        public void AddPatientInWaitingQueueDoctor(SavePatientAndHisWaypoint patient)
        {
            _waitingQueueWaitingDoctor.Enqueue(patient);
            _uiScoreManager.UpdateNbPatientWaiting(NbCaractersInWaiting);
            CheckOrWaitToWaitingDoctorRoom();
        }
        public void AddPatientInDoctorQueue(SavePatientAndHisWaypoint patient)
        { 
            _waitingQueueDoctor.Enqueue(patient);
            _uiScoreManager.UpdateNbPatientDoctor(_waitingQueueDoctor.Count);
            CheckOrWaitToDoctorRoom();
        }
        public void AddPatientToRemoveQueue(SavePatientAndHisWaypoint patient){
            _waitingQueueRemove.Enqueue(patient);
            CheckOrWaitToRemove();
        }

        public void UpdateNbReceptionRemoveFix()
        {
            NbCaractersInReception -= 1;
            _uiScoreManager.UpdateNbPatientReception(NbCaractersInReception);
        }

        public void UpdateNbReceptionAddFix()
        {
            NbCaractersInReception += 1;
            _uiScoreManager.UpdateNbPatientReception(NbCaractersInReception);
        }

        public void UpdateNbWaitingRemoveFix()
        {
            NbCaractersInWaiting -= 1;
            _uiScoreManager.UpdateNbPatientWaiting(NbCaractersInWaiting);
        }

        public void UpdateNbWaitingAddFix()
        {
            NbCaractersInWaiting += 1;
            _uiScoreManager.UpdateNbPatientWaiting(NbCaractersInWaiting);
        }
    }
}
