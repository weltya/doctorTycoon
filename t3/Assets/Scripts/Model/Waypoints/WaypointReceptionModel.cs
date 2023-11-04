using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Model.Waypoints
{
    public class WaypointReceptionModel : IWaypointModel
    {
        private static WaypointReceptionModel _instance;
        private readonly HashSet<Transform> _availableDesk = new();


        private WaypointReceptionModel()
        {
            
        }
        public static WaypointReceptionModel GetInstance()
        {
            _instance ??= new WaypointReceptionModel();
            return _instance;
        }


        public HashSet<Transform> GetWaypoints()
        {
            return _availableDesk;
        }

        public void AddWaypoint(Transform waypoint)
        {
            _availableDesk.Add(waypoint);
        }

        public Transform RequestChair()
        {
            if (_availableDesk.Count > 0)
            {
                var chair = _availableDesk.First();
                _availableDesk.Remove(chair);
                return chair;
            }
            return null;
        }

        public void ReleaseChair(Transform chair)
        {
            _availableDesk.Add(chair);
        }
    }
}

