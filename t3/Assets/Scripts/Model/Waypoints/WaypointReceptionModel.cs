using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Model.Waypoints
{
    public class WaypointReceptionModel
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

        public Transform RequestDesk()
        {
            Random random = new();

            Debug.Log(_availableDesk.Count());

            var desk = _availableDesk.ElementAt(random.Next(0, _availableDesk.Count() - 1));

            if (desk) {
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

