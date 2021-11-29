using UnityEngine;
using UnityEngine.Events;

using Gesture;

namespace River {

public class DriftingItem : MonoBehaviour {

    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D col;
    [SerializeField] protected DriftingItemData data;
    [SerializeField] protected Boat boat;
    [SerializeField] protected bool grabbed;
    protected ADriftingPattern driftingPattern;
    protected AGesture gesture;
    protected bool paused;

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
        col = GetComponent<BoxCollider2D>();
        grabbed = false;
        paused = false;
    }

    void Update() {
        if (driftingPattern != null && !paused) {
            driftingPattern.UpdatePosition(transform);
        }
    }

    public void Grab(Transform grabPos) {
        col.enabled = false;
        grabbed = true;
        paused = true;
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

    public void UpdateSprite() {
        SpriteRenderer _renderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.baseData.sprite;
        Utils.Fuzzy.MatchBoxColliderToSprite(col, _renderer.sprite);
    }

}

}