using UnityEngine;

namespace Controller {
    public class DoctorSpawnerScript : MonoBehaviour
    {
        public GameObject doctor;

        void Start() {
            //spawn();
        }

        void spawn(int x = 0, int y = 0, int z = 0) {
            Instantiate(doctor, new Vector3(x, y, z), transform.rotation);
        }
    }
}