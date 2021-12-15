using UnityEngine;

namespace River {
    public class RiverEnd : MonoBehaviour {

        void OnCollisionEnter2D(Collision2D other) {
            Destroy(other.gameObject);
        }
    }
}