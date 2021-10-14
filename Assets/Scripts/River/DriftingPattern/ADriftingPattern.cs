using UnityEngine;

namespace River {
public abstract class ADriftingPattern {

    protected DriftingPatternData setting;
    protected float curSpeed;

    public abstract void UpdatePosition(Transform transform);

    public void Init(DriftingPatternData data) {
        setting = data;
        curSpeed = UnityEngine.Random.Range(setting.minSpeed, setting.maxSpeed);
    }

}

}