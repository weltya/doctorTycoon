using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model.Waypoints
{
    public interface IWaypointModel
    {
        public HashSet<Transform> GetWaypoints();
        public void AddWaypoint(Transform waypoint);
        public Transform RequestChair();
        public void ReleaseChair(Transform chair);
    }
}

