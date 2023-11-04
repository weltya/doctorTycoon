using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Model.Utils;

namespace Model.Waypoints
{
    public class WaypointModel : IWaypointModel
    {
        private static WaypointModel _instance;
        private readonly Dictionary<PatientState, HashSet<Transform>> _waypointsByType = new Dictionary<PatientState, HashSet<Transform>>();

        private WaypointModel() { }

        public static WaypointModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WaypointModel();
            }
            return _instance;
        }

        public void AddWaypoint(PatientState type, Transform waypoint)
        {
            if (!_waypointsByType.ContainsKey(type))
            {
                _waypointsByType[type] = new HashSet<Transform>();
            }

            _waypointsByType[type].Add(waypoint);
        }

        public Transform RequestWaypoint(PatientState type)
        {
            if (_waypointsByType.TryGetValue(type, out var waypoints) && waypoints.Count > 0)
            {
                var waypoint = waypoints.First();
                waypoints.Remove(waypoint);
                return waypoint;
            }
            return null;
        }

        public void ReleaseWaypoint(PatientState type, Transform waypoint)
        {
            if (_waypointsByType.ContainsKey(type))
            {
                _waypointsByType[type].Add(waypoint);
            }
        }

        public HashSet<Transform> GetWaypoints(PatientState state)
        {
            if (_waypointsByType.TryGetValue(state, out var waypoints))
            {
                return waypoints;
            }
            return new HashSet<Transform>();
        }
    }
}
