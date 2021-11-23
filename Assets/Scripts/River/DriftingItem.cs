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
    protected bool grabbed;

    public AGesture Gesture {
        get => gesture;
        set {
            gesture = value;
            gesture.OnFailed.AddListener(GestureFailed);
            gesture.OnSatisfied.AddListener(GestureSatisfied);
        }
    }

    public Boat Boat {
        get => boat;
        set => boat = value;
    }

    public ADriftingPattern DriftingPattern {
        get => driftingPattern;
        set => driftingPattern = value;
    }

    public DriftingItemData DriftingItemData {
        get => data;
        set => data = value;
    }

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        grabbed = false;
    }

    void Update() {
        if (driftingPattern != null && !grabbed) {
            driftingPattern.UpdatePosition(transform);
        }
    }

    public void Grab(Transform grabPos) {
        col.enabled = false;
        grabbed = true;
        transform.SetParent(grabPos);
        transform.localPosition = Vector3.zero;
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