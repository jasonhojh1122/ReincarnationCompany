
using UnityEngine;

public class GrabPosition : MonoBehaviour {

    [SerializeField] Gesture.GestureManager gestureManager;
    Collider col;
    River.DriftingItem driftingItem;
    Gesture.AGesture gesture;

    private void Start() {
        gestureManager = FindObjectOfType<Gesture.GestureManager>();
        col = GetComponent<Collider>();
    }


    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Monster") {
            Debug.Log("Grabbed");
            Grab(other.gameObject);
        }
    }

    void Grab(GameObject go) {
            driftingItem = go.GetComponent<River.DriftingItem>();
            driftingItem.ToggleDrifting(false);
            driftingItem.transform.SetParent(this.transform);
            driftingItem.transform.localPosition = Vector3.zero;
            driftingItem.Grab();

            gesture = driftingItem.GetGesture();
            gesture.OnFailed += EnemyFailed;
            gesture.OnSatisfied += LetGo;
            gestureManager.Enqueue(gesture);

            col.enabled = false;
    }

    public void EnemyFailed() {
        Debug.Log("Failed");
        driftingItem.ToggleDrifting(true);
        col.enabled = true;
    }

    public void LetGo() {
        Debug.Log("LetGo");
        driftingItem.ToggleDrifting(true);
        driftingItem.transform.SetParent(null);
        col.enabled = true;
    }


}