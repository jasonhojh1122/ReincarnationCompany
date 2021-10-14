using UnityEngine;

namespace River {
    public class Straight : ADriftingPattern {

        public override void UpdatePosition(Transform transform) {
            Vector3 newPos = transform.position;
            newPos.x -= curSpeed * Time.deltaTime;
            transform.position = newPos;
        }

    }

}