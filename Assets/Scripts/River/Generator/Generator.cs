using UnityEngine;
using System.Collections.Generic;

namespace River {
public class Generator : MonoBehaviour {
    [SerializeField] protected DriftingItem prefab;
    [SerializeField] protected List<DriftingItemData> itemPool;
    [SerializeField] CDF cdf;
    protected DriftingPatternPool driftingPatternPool;

    private void Awake() {
        cdf.CalculateCDF();
    }

    public void SetDriftingPatternPool(DriftingPatternPool pool) {
        driftingPatternPool = pool;
    }

    public DriftingItem Generate() {

        DriftingItem di = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        int itemID = cdf.GetRandomID();
        di.DriftingItemData = itemPool[itemID];

        DriftingPatternData patternData = itemPool[itemID].GetRandomDriftingPattern();
        if (patternData != null) {
            di.DriftingPattern = driftingPatternPool.InstantiateDriftingPattern(patternData);
            di.DriftingPattern.Init(patternData, di.DriftingItemData);
        }


        Gesture.GestureData gestureData = itemPool[itemID].GetRandomGesture();
        if (gestureData != null)
            di.Gesture = Gesture.GestureManager.Instance.GesturePool.InstantiateGesture(gestureData);

        di.UpdateSprite();

        return di;
    }

}

}