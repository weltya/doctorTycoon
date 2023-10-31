using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model.Waypoints
{
    public class WaitingRoomWayointModel : MonoBehaviour
    {
        private List<Transform> _chairsPosition;
        private HashSet<Transform> _availableChairs;

        private void Awake()
        {
            _chairsPosition = new List<Transform>();
            _availableChairs = new HashSet<Transform>();
            FillHashSet();
        }

        public void FillHashSet()
        {
            Transform wayPointsWaitingRoom = transform.Find("WayPointsWaitingRoom");

            if (wayPointsWaitingRoom == null)
            {
                Debug.LogError("WayPointsWaitingRoom not found!");
                return;
            }

            foreach (Transform wayPointsSeat in wayPointsWaitingRoom)
            {
                foreach (Transform waitingRoomWayPoint in wayPointsSeat)
                {
                    _availableChairs.Add(waitingRoomWayPoint);
                }
            }
        }


        public Transform RequestChair()
        {
            Debug.Log("hey");
            foreach (var  chair in _availableChairs)
            {
                _availableChairs.Remove(chair);
                return chair;
            }

            return null;
        }

        public void ReleaseChair(Transform chair)
        {
            _availableChairs.Add(chair);
        }

    }
}

