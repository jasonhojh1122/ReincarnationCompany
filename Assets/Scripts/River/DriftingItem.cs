using UnityEngine;
using UnityEngine.Events;

namespace River {

public class DriftingItem : MonoBehaviour {

    protected SpriteRenderer spriteRenderer;
    protected DriftingItemData data;
    protected ADriftingPattern driftingPattern;

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

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "RiverEnd") {
            Destroy(gameObject);
        }
    }

}

}