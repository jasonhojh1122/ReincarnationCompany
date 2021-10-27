using UnityEngine;
using System.Collections.Generic;

namespace River {
public class Generator : MonoBehaviour {

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

    public DriftingItem Generate() {

        DriftingItem go = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        int itemID = UnityEngine.Random.Range(0, itemPool.Count);
        go.SetData(itemPool[itemID]);

        DriftingPatternData patternData = itemPool[itemID].GetRandomDriftingPattern();
        go.SetDriftingPattern(driftingPatternPool.InstantiateDriftingPattern(patternData));

        Gesture.GestureData gestureData = itemPool[itemID].GetRandomGesture();
        go.SetGesture(gesturePool.InstantiateGesture(gestureData));

        return go;
    }

}

}