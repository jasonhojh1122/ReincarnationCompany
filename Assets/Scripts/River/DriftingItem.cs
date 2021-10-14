using UnityEngine;
using UnityEngine.Events;

namespace River {

public class DriftingItem : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private DriftingItemData data;
    // public UnityAction<Transform, float> UpdatePos;
    private ADriftingPattern driftingPattern;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (driftingPattern != null) {
            driftingPattern.UpdatePosition(transform);
        }
    }

    public void SetData(DriftingItemData data) {
        this.data = data;
        spriteRenderer.sprite = data.sprite;
    }

    public void SetDriftingPattern(ADriftingPattern pattern) {
        driftingPattern = pattern;
    }

}

}