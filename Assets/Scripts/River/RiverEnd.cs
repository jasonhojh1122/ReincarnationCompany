using UnityEngine;

namespace River {
    public class RiverEnd : MonoBehaviour {

        [SerializeField] BoatLife boatLife;
        void OnCollisionEnter2D(Collision2D other) {
            Destroy(other.gameObject);
        }
    }
}