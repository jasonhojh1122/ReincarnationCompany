using UnityEngine;

namespace River {
    public class Straight : ADriftingPattern {

        public Straight(float minZ, float maxZ, float minSpeed, float maxSpeed) :
            base(minZ, maxZ, minSpeed, maxSpeed) {
            curSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        }

        public override void UpdatePosition(Transform transform) {
            Vector3 newPos = transform.position;
            newPos.x -= curSpeed * Time.deltaTime;
            transform.position = newPos;
        }

    }

}