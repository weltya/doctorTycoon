using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Model.Waypoints
{
    public class WaypointReceptionModel
    {
        private static WaypointReceptionModel _instance;
        private HashSet<Transform> _availableDesk = new();


        private WaypointReceptionModel() 
        {
            
        }
        public static WaypointReceptionModel GetInstance()
        {
            if ( _instance == null )
            {
                _instance = new WaypointReceptionModel();
            }
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

        public Transform RequestDesk()
        {
            if (_availableDesk.Count > 0)
            {
                var desk = _availableDesk.First();
                _availableDesk.Remove(desk);
                return desk;
            }
            return null;
        }

        public void ReleaseDesk(Transform desk)
        {
            _availableDesk.Add(desk);
        }
    }
}

