using UnityEngine;

namespace River {
public abstract class ADriftingPattern {

    protected DriftingPatternData setting;
    protected float curSpeed;

    public abstract void UpdatePosition(Transform transform);

    public void Init(DriftingPatternData dpd, DriftingItemData did) {
        setting = dpd;
        curSpeed = UnityEngine.Random.Range(did.minSpeed, did.maxSpeed);
    }

}

}