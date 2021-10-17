using UnityEngine;
using System.Collections.Generic;

namespace River {
public abstract class AGenerator : MonoBehaviour {

    [SerializeField] protected DriftingItem prefab;
    [SerializeField] protected List<DriftingItemData> itemPool;
    protected DriftingPatternPool driftingPatternPool;
    protected Gesture.GesturePool gesturePool;

    public void SetDriftingPatternPool(DriftingPatternPool pool) {
        driftingPatternPool = pool;
    }

    public void SetGesturePool(Gesture.GesturePool pool) {
        gesturePool = pool;
    }

    public abstract DriftingItem Generate();

}

}