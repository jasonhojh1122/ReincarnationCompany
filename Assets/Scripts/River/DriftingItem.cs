using UnityEngine;
using UnityEngine.Events;

using Gesture;

namespace River {

public class DriftingItem : MonoBehaviour {

    protected SpriteRenderer spriteRenderer;
    protected Collider2D col;
    protected DriftingItemData data;
    protected ADriftingPattern driftingPattern;
    protected AGesture gesture;
    protected Boat boat;
    protected bool drifting;
    protected bool grabbed;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        drifting = true;
        grabbed = false;
    }

    void Update() {
        if (driftingPattern != null && drifting) {
            driftingPattern.UpdatePosition(transform);
        }
    }

    public void SetData(DriftingItemData data) {
        this.data = data;
        spriteRenderer.sprite = data.baseData.sprite;
    }

    public void SetBoat(Boat boat) {
        this.boat = boat;
    }

    public DriftingItemData GetData() {
        return data;
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

    public void ToggleDrifting() {
        drifting = !drifting;
    }

    public void Grab() {
        col.enabled = false;
        grabbed = true;
    }

    public virtual void GestureFailed() {
        Debug.Log("Failed");
        Destroy(this.gameObject);
    }

    public virtual void GestureSatisfied() {
        Debug.Log("Satisfied");
        Destroy(this.gameObject);
    }

    public bool IsGrabbed() {
        return grabbed;
    }

}

}