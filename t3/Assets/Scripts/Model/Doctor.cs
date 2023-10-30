using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public class Medecin : MonoBehaviour
    {
        public Transform[] waypoints;
        public float speed = 5.0f;
        private int currentWaypointIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            MoveToWaypoints();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        IEnumerator MoveToWaypoints() {
            while (currentWaypointIndex < waypoints.Length) {
                Debug.Log("1");
                Vector3 targetPosition = waypoints[currentWaypointIndex].position;

                while (Vector3.Distance(transform.position, targetPosition) > 0) {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                    Debug.Log(transform.position.x.ToString());
                    yield return null;
                }

                currentWaypointIndex++;
                if (currentWaypointIndex < waypoints.Length)
                    yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
