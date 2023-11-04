using Model.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Waypoints
{
    public interface IWaypointModel
    {
        public HashSet<Transform> GetWaypoints(PatientState state);
        public void AddWaypoint(PatientState type, Transform waypoint);
        public Transform RequestWaypoint(PatientState type);
        public void ReleaseWaypoint(PatientState type, Transform waypoint);
    }
}

