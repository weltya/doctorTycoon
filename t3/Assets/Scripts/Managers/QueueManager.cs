using Scripts.Gameplay.Caracters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers.BuilderManagers;


namespace Scripts.Managers.Caracters
{
    public class QueueManager : MonoBehaviour
    {
        private static QueueManager Instance;
        private ReceptionRoomData reception;
        private Queue<GameObject> _waitingQueueReception = new Queue<GameObject>();
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

        public void CheckOrWaitToReception(GameObject patientGameplay)
        {
            if (IsReceptionIsAvailable())
            {
                reception.available = false;
                MoveToReception(patientGameplay);
            }
            else
            {
                _waitingQueueReception.Enqueue(patientGameplay);
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
                WaitingRoomData WaitingRoom = IsWaitingRoomAvailable();
                if (WaitingRoom != null)
                {
                    int i = 0;
                    _waitingQueueDoctor.Enqueue(patientGameplay);
                    patientGameplay.MovePatientToWaitingRoom(WaitingRoom,i);
                }
            }
        }

        public void CheckOrWaitToWaitingRoomDoctor(PatientGameplay patientGameplay)
        {
            DoctorRoomData whereToGo = IsDoctorIsAvailable();

            if (whereToGo != null)
            {
                patientGameplay.MovePatientToDoctor(whereToGo);
            }
            else
            {
                WaitingRoomData WaitingRoom = IsWaitingRoomAvailable();
                if (WaitingRoom != null)
                {
                    int i = 0;
                    _waitingQueueDoctor.Enqueue(patientGameplay);
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
                if (ListWaitingRoom[i].patients.Count < ListWaitingRoom[i].capacity)
                {
                    return ListWaitingRoom[i];
                }
            }
            return null;
        }



        private void UpdateReceptionAvailability()
        {
            ReceptionRoomData wheretogo=IsReceptionIsAvailable();
            if (IsReceptionIsAvailable() && _waitingQueueReception.Count > 0)
            {
                MoveToReception(_waitingQueueReception.Dequeue());
            }
        }
        private void UpdateNurseAvailability()
        {
            NurseRoomData whereToGo = IsNurseIsAvailable();
            if ((whereToGo != null) && (_waitingQueueNurse.Count > 0))
            {
                _waitingQueueNurse.Dequeue().MovePatientToNurse(whereToGo);
            }
        }
        private void UpdateDoctorAvailability()
        {
            DoctorRoomData whereToGo = IsDoctorIsAvailable();

            if ((whereToGo != null) && (_waitingQueueDoctor.Count > 0))
            {
                _waitingQueueDoctor.Dequeue().MovePatientToDoctor(whereToGo);
            }
        }



        private void MoveToReception(GameObject patientGameplay)
        {
            PatientGameplay scriptPatientGameplay = patientGameplay.GetComponent<PatientGameplay>();
            if (scriptPatientGameplay == null) { Debug.LogError("scriptPatientGameplay equal to null"); }
            scriptPatientGameplay.MovePatientToReception(reception);
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

            Debug.Log(ListDoctor);

        }
    }
}

