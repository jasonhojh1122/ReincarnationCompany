
using UnityEngine;

public class GrabPosition : MonoBehaviour {

    [SerializeField] Gesture.GestureManager gestureManager;
    [SerializeField] Boat boat;
    Collider2D col;
    River.DriftingItem driftingItem;
    Gesture.AGesture gesture;

    private void Start() {
        gestureManager = FindObjectOfType<Gesture.GestureManager>();
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

            driftingItem.ToggleDrifting();
            driftingItem.transform.SetParent(this.transform);
            driftingItem.transform.localPosition = Vector3.zero;
            driftingItem.Grab();

            gesture = driftingItem.GetGesture();
            if (go.gameObject.tag == "Monster") {
                gesture.OnFailed += MonsterFailed;
            }
            gesture.OnFailed += Failed;
            gesture.OnFailed += driftingItem.GestureFailed;

            gesture.OnSatisfied += Satisfied;
            gesture.OnSatisfied += driftingItem.GestureSatisfied;

            gestureManager.Enqueue(gesture);

            col.enabled = false;
    }

    public void SetBoat(Boat boat) {
        this.boat = boat;
    }

    public void MonsterFailed() {
        boat.AddToLife(-1);
    }

    public void Failed() {
        col.enabled = true;
    }

    public void Satisfied() {
        col.enabled = true;
        boat.AddToMoney(driftingItem.GetData().pikeUpPrice);
    }


}