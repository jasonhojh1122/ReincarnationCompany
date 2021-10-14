using UnityEngine;

namespace River {
public abstract class ADriftingPattern {

    protected float minZ, maxZ;
    protected float minSpeed, maxSpeed, curSpeed;

    public ADriftingPattern(float minZ, float maxZ, float minSpeed, float maxSpeed) {
        this.minZ = minZ;
        this.maxZ = maxZ;
        this.minSpeed = minSpeed;
        this.maxSpeed = maxSpeed;
    }

    public abstract void UpdatePosition(Transform transform);

}

}