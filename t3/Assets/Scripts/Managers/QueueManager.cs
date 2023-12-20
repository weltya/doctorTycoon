using Scripts.Gameplay.Caracters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers.BuilderManagers;
using System.Linq;


namespace Scripts.Managers.Caracters
{
    public class QueueManager : MonoBehaviour
    {
        private static QueueManager Instance;
        private ReceptionRoomData reception;
        private Queue<PatientGameplay> _waitingQueueReception = new Queue<PatientGameplay>();
        private Queue<PatientGameplay> _waitingQueueNurse = new Queue<PatientGameplay>();
        private Queue<PatientGameplay> _waitingQueueDoctor = new Queue<PatientGameplay>();

        private List<ReceptionRoomData> ListReceptionRoom = new List<ReceptionRoomData>();
        private List<WaitingRoomData> ListWaitingRoom = new List<WaitingRoomData>();
        private List<DoctorRoomData> ListDoctor = new List<DoctorRoomData>();
        private List<NurseRoomData> ListNurse = new List<NurseRoomData>();
        [SerializeField] ObjectPlacer _objectPlacer;

        private void Start()
        {
            _objectPlacer.onObjectPlaced += AddRoom;
        }
        private void Awake()
        {
            Instance = this;
        }
        public static QueueManager GetInstance()
        {
            return Instance;
        }
        private void Update(){
            UpdateReceptionAvailability();
            UpdateNurseAvailability();
            UpdateDoctorAvailability();
        }

        public void CheckOrWaitToReception(GameObject patientGameplay)
        {

            PatientGameplay scriptPatientGameplay = patientGameplay.GetComponent<PatientGameplay>();

            if (scriptPatientGameplay == null) {
                 Debug.LogError("scriptPatientGameplay equal to null"); 
            }

            ReceptionRoomData wheretogo=IsReceptionIsAvailable();
            if (wheretogo)
            {
                wheretogo.available = false;
                scriptPatientGameplay.MovePatientToReception(wheretogo);
            }
            else
            {
                _waitingQueueReception.Enqueue(scriptPatientGameplay);
            }
        }

        public void CheckOrWaitToWaitingRoomNurse(PatientGameplay patientGameplay)
        {
            NurseRoomData whereToGo = IsNurseIsAvailable();
            if (whereToGo != null)
            {
                patientGameplay.MovePatientToNurse(whereToGo);
            }
            else
            {
                _waitingQueueNurse.Enqueue(patientGameplay);
                WaitingRoomData WaitingRoom = IsWaitingRoomAvailable();
                if (WaitingRoom != null)
                {
                    int i;
                    for(i=0;i<WaitingRoom.points.Count;i++){
                        if(WaitingRoom.points.ElementAt(i).Value)
                        {
                            WaitingRoom.points[WaitingRoom.points.ElementAt(i).Key]=false;
                            break;
                        }
                    }    
                    patientGameplay.MovePatientToWaitingRoom(WaitingRoom,i);
                }
            }
        }

        public void CheckOrWaitToWaitingRoomDoctor(PatientGameplay patientGameplay)
        {
            DoctorRoomData whereToGo = IsDoctorIsAvailable();

            if (whereToGo != null)
            {
                Debug.Log(_waitingQueueDoctor.Dequeue().Name);
                Debug.Log(patientGameplay.Name);
                patientGameplay.MovePatientToDoctor(whereToGo);
            }
            else
            {
                _waitingQueueDoctor.Enqueue(patientGameplay);
                WaitingRoomData WaitingRoom = IsWaitingRoomAvailable();
                if (WaitingRoom != null)
                {   
                    int i;
                    for(i=0;i<WaitingRoom.points.Count;i++){
                        if(WaitingRoom.points.ElementAt(i).Value)
                        {
                            WaitingRoom.points[WaitingRoom.points.ElementAt(i).Key]=false;
                            break;
                        }
                    }    
                    patientGameplay.MovePatientToWaitingRoom(WaitingRoom,i);
                }

            }
        }
        public ReceptionRoomData IsReceptionIsAvailable()
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
        private WaitingRoomData IsWaitingRoomAvailable()
        {
            for (int i = 0; i < ListWaitingRoom.Count; i++)
            {
                if (ListWaitingRoom[i].points.Count < ListWaitingRoom[i].capacity)
                {
                  return ListWaitingRoom[i];
                }
            }
            return null;
        }

        private void UpdateReceptionAvailability()
        {        
            ReceptionRoomData wheretogo=IsReceptionIsAvailable();
            if (wheretogo!=null && _waitingQueueReception.Count > 0)
            {
                wheretogo.available=false;
                _waitingQueueReception.Dequeue().MovePatientToReception(wheretogo);
            }
        }
        private void UpdateNurseAvailability()
        {
            NurseRoomData whereToGo = IsNurseIsAvailable();
            if ((whereToGo != null) && (_waitingQueueNurse.Count > 0))
            {              
                whereToGo.available=false;
                _waitingQueueNurse.Dequeue().MovePatientToNurse(whereToGo);
            }
        }
        private void UpdateDoctorAvailability()
        {
            DoctorRoomData whereToGo = IsDoctorIsAvailable();

            if ((whereToGo != null) && (_waitingQueueDoctor.Count > 0))
            {
                whereToGo.available=false;
                _waitingQueueDoctor.Dequeue().MovePatientToDoctor(whereToGo);
            }
        }
        private void AddRoom(Room room)
        {
            
            if(room is DoctorRoomData){
                ListDoctor.Add((DoctorRoomData)room);
            }
            else if(room is WaitingRoomData){
                ListWaitingRoom.Add((WaitingRoomData)room);
            }else if(room is NurseRoomData){
                ListNurse.Add((NurseRoomData)room);
            }else if(room is ReceptionRoomData){
                ListReceptionRoom.Add((ReceptionRoomData)room);
            } 

            Debug.Log($"nbrReception={ListReceptionRoom.Count}, nbrWaiting={ListWaitingRoom.Count}, " +
                $"nbrNurse={ListNurse.Count}, nbrDoctor={ListDoctor.Count}");

        }
    }
}

