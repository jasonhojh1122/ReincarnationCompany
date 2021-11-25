
using UnityEngine;

namespace River {
    public class GrabPosition : MonoBehaviour {

        [SerializeField] Boat boat;
        Collider2D col;
        River.DriftingItem driftingItem;

        private void Awake() {
            col = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.tag == "Monster" || other.gameObject.tag == "Ingredient") {
                Grab(other.gameObject);
            }
        }

        void Grab(GameObject go) {
            driftingItem = go.GetComponent<River.DriftingItem>();
            if (driftingItem.IsGrabbed())
                return;

            driftingItem.Grab(transform);

            var gesture = driftingItem.Gesture;
            gesture.OnFailed.AddListener(OnFinished);
            gesture.OnSatisfied.AddListener(OnFinished);
            gesture.OnSingleSatisfied.AddListener(boat.Indicator.UpdateCount);
            gesture.OnStart.AddListener(delegate{boat.ShowIndicator(gesture);});
            gesture.OnReset.AddListener(boat.Indicator.Reset);

            Gesture.GestureManager.Instance.Enqueue(gesture);

            col.enabled = false;
        }

        public void OnFinished() {
            boat.Indicator.gameObject.SetActive(false);
            col.enabled = true;
        }

        public void SetBoat(Boat boat) {
            this.boat = boat;
        }

    }
}