using Scripts.Gameplay.Caracters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers.Caracters
{
    public class QueueManager : MonoBehaviour
    {
        private static QueueManager Instance;

        private Queue<GameObject> _waitingQueueReception = new Queue<GameObject>();
        

        #region[unity function]
        private void Awake ()
        {
            Instance = this;
        }
        #endregion[unity function]

        #region[instance]
        public static QueueManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new QueueManager();
            }
            return Instance;
        }
        #endregion[instance]

        #region[check or wait]
        public void CheckOrWaitToReception(GameObject patientGameplay)
        {
            if (IsReceptionIsAvailable())
            {
                MoveToReception(patientGameplay);
            } else
            {
                _waitingQueueReception.Enqueue(patientGameplay);
            }
        }

        #endregion[check or wait]

        #region[private check or wait]
        private bool IsReceptionIsAvailable()
        {
            return true;
        }

        private void UpdateReceptionAvailability()
        {
            if (IsReceptionIsAvailable() && _waitingQueueReception.Count > 0)
            {
                MoveToReception(_waitingQueueReception.Dequeue());
            }
        }

        private void MoveToReception(GameObject patientGameplay)
        {
            PatientGameplay scriptPatientGameplay = patientGameplay.GetComponent<PatientGameplay>();
            if (scriptPatientGameplay != null) { Debug.LogError("scriptPatientGameplay equal to null"); }
            scriptPatientGameplay.MovePatientToreception();

        }
        #endregion[private check or waim]
    }
}

