using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Model.Waypoints
{
    public class WaypointWaitingRoomNurseModel : IWaypointModel
    {
        private static WaypointWaitingRoomNurseModel _instance;
        //private List<Transform> _waypoints = new List<Transform>();
        private HashSet<Transform> _availableChairs = new HashSet<Transform>();


        private WaypointWaitingRoomNurseModel() 
        {
            
        }
        public static WaypointWaitingRoomNurseModel GetInstance()
        {
            if ( _instance == null )
            {
                _instance = new WaypointWaitingRoomNurseModel();
            }
            return _instance;
        }


        public HashSet<Transform> GetWaypoints()
        {
            return _availableChairs;
        }

        public void AddWaypoint(Transform waypoint)
        {
            _availableChairs.Add(waypoint);
        }

        public Transform RequestChair()
        {
            if (_availableChairs.Count > 0)
            {
                var chair = _availableChairs.First();
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
