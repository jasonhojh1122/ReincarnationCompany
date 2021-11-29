using UnityEngine;

namespace River {
    public class DriftingStone : DriftingItem {

        bool hasCollided;

        private void Start() {
            grabbed = true;
            hasCollided = false;
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.tag != "Player" || hasCollided) return;
            hasCollided = true;
            boat.AddToLife(-1);
        }

    }
}