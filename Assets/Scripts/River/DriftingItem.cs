using UnityEngine;
using UnityEngine.Events;

using Gesture;

namespace River {

public class DriftingItem : MonoBehaviour {

    protected SpriteRenderer spriteRenderer;
    protected Collider col;
    protected DriftingItemData data;
    protected ADriftingPattern driftingPattern;
    protected AGesture gesture;
    protected bool drifting;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider>();
        drifting = true;
    }

    void Update() {
        if (driftingPattern != null && drifting) {
            driftingPattern.UpdatePosition(transform);
        }
    }

    public void SetData(DriftingItemData data) {
        this.data = data;
        spriteRenderer.sprite = data.sprite;
    }

    public void SetDriftingPattern(ADriftingPattern driftingPattern) {
        this.driftingPattern = driftingPattern;
    }

    public void SetGesture(AGesture gesture) {
        this.gesture = gesture;
    }

    public AGesture GetGesture() {
        return gesture;
    }

    public void ToggleDrifting(bool status) {
        drifting = status;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "RiverEnd") {
            Destroy(gameObject);
        }
    }

    public void Grab() {
        col.enabled = false;
    }

    public void Sink() {
        Debug.Log("Sink");
    }

}

}