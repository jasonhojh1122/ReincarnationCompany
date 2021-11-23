using UnityEngine;

namespace River {
    public class RiverEnd : MonoBehaviour {

        [SerializeField] BoatLife boatLife;
        void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.tag == "Monster") {
                boatLife.AddToLife(-1);
            }
            Destroy(other.gameObject);
        }
    }
}