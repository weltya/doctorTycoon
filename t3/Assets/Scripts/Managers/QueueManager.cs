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
        private static QueueManager Instance;
        private ReceptionRoomData reception;
        private Queue<PatientGameplay> _waitingQueueReception = new Queue<PatientGameplay>();
        private Queue<PatientGameplay> _waitingQueueWaitingNurse = new Queue<PatientGameplay>();
        private Queue<PatientGameplay> _waitingQueueNurse = new Queue<PatientGameplay>();
        private Queue<PatientGameplay> _waitingQueueWaitingDoctor = new Queue<PatientGameplay>();
        private Queue<PatientGameplay> _waitingQueueDoctor = new Queue<PatientGameplay>();

        private List<ReceptionRoomData> ListReceptionRoom = new List<ReceptionRoomData>();
        private List<WaitingRoomData> ListWaitingRoom = new List<WaitingRoomData>();
        private List<DoctorRoomData> ListDoctor = new List<DoctorRoomData>();
        private List<NurseRoomData> ListNurse = new List<NurseRoomData>();
        [SerializeField] ObjectPlacer _objectPlacer;
        [SerializeField] private  UIScoreManager score;
     
    
        public struct WaitingRoomStruct
        {
            public WaitingRoomData waitingRoom;
            public PointData pointData;
        }

      

        private void Start()
        {   
            
            _objectPlacer.onObjectPlaced += AddRoom;
          
        }
        private void Awake()
        {
            Instance = this;
        }
        
        private ReceptionRoomData IsReceptionIsAvailable()
        {
            for (int i = 0; i < ListReceptionRoom.Count; i++)
            {
                if (ListReceptionRoom[i].available)
                {
                    ListReceptionRoom[i].available = false;
                    return ListReceptionRoom[i];
                }
            }
            return null;
        }
        private DoctorRoomData IsDoctorIsAvailable()
        {
            for (int i = 0; i < ListDoctor.Count; i++)
            {
                if (ListDoctor[i].available)
                {
                    ListDoctor[i].available = false;
                    return ListDoctor[i];
                }
            }
            return null;
        }
        private NurseRoomData IsNurseIsAvailable()
        {
            for (int i = 0; i < ListNurse.Count; i++)
            {
                if (ListNurse[i].available)
                {
                    ListNurse[i].available = false;
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
                if (waitingRoomData.capacity >= waitingRoomData.maxCapacity)
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
        if(score!=null)
        {   
            if(room is DoctorRoomData)
            {
                ListDoctor.Add((DoctorRoomData)room);
                score.UpdateDoctor(ListDoctor.Count);
                    CheckOrWaitToDoctorRoom();
            } else if(room is WaitingRoomData)
            {
                ListWaitingRoom.Add((WaitingRoomData)room);
                score.UpdateRoom(ListWaitingRoom.Count);
                CheckOrWaitToWaitingNurseRoom();
                CheckOrWaitToWaitingDoctorRoom();
            } else if(room is NurseRoomData)
            {
                ListNurse.Add((NurseRoomData)room);
                score.UpdateNurse(ListNurse.Count);
                CheckOrWaitToNurseRoom();
            } else if(room is ReceptionRoomData)
            {
                ListReceptionRoom.Add((ReceptionRoomData)room);
                score.UpdateReception(ListReceptionRoom.Count);
                CheckOrWaitToReception();
            } 
         }

            //Debug.Log($"nbrReception={ListReceptionRoom.Count}, nbrWaiting={ListWaitingRoom.Count}, " +
                //$"nbrNurse={ListNurse.Count}, nbrDoctor={ListDoctor.Count}");
        }

        public static QueueManager GetInstance()
        {
            return Instance;
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
                wheretogo.available = false;
                _waitingQueueReception.Dequeue();

                score.UpdatePatientInReception(_waitingQueueReception);
                patient.MovePatientToReception(wheretogo);
                
            }
        }

        public void CheckOrWaitToWaitingNurseRoom()
        {
            if (_waitingQueueWaitingNurse.Count <= 0)
            {
                return;
            }

            if (_waitingQueueWaitingNurse.Count <= 0)
            {
              return;
              }

            PatientGameplay patient = _waitingQueueWaitingNurse.Peek();
            WaitingRoomStruct whereToGo = IsWaitingRoomAvailable();

            if (whereToGo.waitingRoom != null)
            {
                whereToGo.pointData.IsAvailable = false;
                patient.MovePatientToWaitingNurse(whereToGo.waitingRoom, whereToGo.pointData);
                _waitingQueueWaitingNurse.Dequeue();
            }
        }

        public void CheckOrWaitToNurseRoom()
        {
            if (_waitingQueueNurse.Count <= 0)
            {
                return;
            }

            PatientGameplay patient = _waitingQueueNurse.Peek();
            NurseRoomData whereToGo = IsNurseIsAvailable();

            if (whereToGo != null)
            {
                whereToGo.available = false;
                patient.MovePatientToNurse(whereToGo);
                _waitingQueueNurse.Dequeue();
            }
    
        }

        public void CheckOrWaitToWaitingDoctorRoom()
        {
            if (_waitingQueueWaitingDoctor.Count <= 0)
            {
                return;
            }

            PatientGameplay patient = _waitingQueueWaitingDoctor.Peek();
            WaitingRoomStruct whereToGo = IsWaitingRoomAvailable();

            if (whereToGo.waitingRoom != null)
            {
            
                whereToGo.pointData.IsAvailable = false;
                patient.MovePatientToWaitingDoctor(whereToGo.waitingRoom, whereToGo.pointData);
               
                _waitingQueueWaitingDoctor.Dequeue();
            }
      
        }
        public void CheckOrWaitToDoctorRoom()
        {
            if (_waitingQueueDoctor.Count <= 0)
            {
                return;
            }

            PatientGameplay patient = _waitingQueueDoctor.Peek();
            DoctorRoomData whereToGo = IsDoctorIsAvailable();

            if (whereToGo != null)
            {
                whereToGo.available = false;
                patient.MovePatientToDoctor(whereToGo);
                _waitingQueueDoctor.Dequeue();
            }
        }

        public void AddPatientInSpawnQueue(GameObject go)
        {
            PatientGameplay scriptPatientGameplay = go.GetComponent<PatientGameplay>();
            _waitingQueueReception.Enqueue(scriptPatientGameplay);
            score.UpdatePatientInReception(_waitingQueueReception.Count);
            CheckOrWaitToReception();
        }

        public void AddPatientInWaitingQueueNurse(PatientGameplay patient)
        {
            _waitingQueueWaitingNurse.Enqueue(patient);
            score.UpdatePatientInNurse(_waitingQueueWaitingNurse.Count + _waitingQueueNurse.Count); 
            CheckOrWaitToWaitingNurseRoom();
      
            
        }

        public void AddPatientInNurseQueue(PatientGameplay patient)
        {
            _waitingQueueNurse.Enqueue(patient);
             CheckOrWaitToNurseRoom(); 
         
           
            
        }
        public void AddPatientInWaitingQueueDoctor(PatientGameplay patient)
        { 
            score.UpdatePatientInDoctor(_waitingQueueWaitingDoctor.Count+ _waitingQueueDoctor.Count);
            _waitingQueueWaitingDoctor.Enqueue(patient);
            
            CheckOrWaitToWaitingDoctorRoom();
          
           

        }
        public void AddPatientInDoctorQueue(PatientGameplay patient)
        {
           
            _waitingQueueDoctor.Enqueue(patient);
            
            CheckOrWaitToDoctorRoom();
       
        }

    }
}
