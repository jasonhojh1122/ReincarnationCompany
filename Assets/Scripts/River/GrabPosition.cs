
using UnityEngine;

namespace River {
    public class GrabPosition : MonoBehaviour {

        [SerializeField] Boat boat;
        Collider2D col;
        River.DriftingItem driftingItem;
        static RiverGestureIndicator indicator;

        private void Awake() {
            if (indicator == null) {
                indicator = FindObjectOfType<RiverGestureIndicator>();
            }
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
            gesture.OnSingleSatisfied.AddListener(indicator.UpdateCount);
            gesture.OnStart.AddListener(delegate{indicator.Show(gesture);});
            gesture.OnReset.AddListener(indicator.ResetCount);

            var riverPlayer = (RiverPlayer) GameManager.Instance.SceneSettings.Peek().player;
            gesture.OnStart.AddListener(riverPlayer.Catch);
            gesture.OnFailed.AddListener(riverPlayer.Release);
            gesture.OnSatisfied.AddListener(riverPlayer.Release);

            Gesture.GestureManager.Instance.Enqueue(gesture);

            col.enabled = false;
        }

        public void OnFinished() {
            indicator.End();
            col.enabled = true;
        }

        public void SetBoat(Boat boat) {
            this.boat = boat;
        }

    }
}