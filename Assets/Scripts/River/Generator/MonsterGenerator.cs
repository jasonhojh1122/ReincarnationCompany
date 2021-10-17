using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace River {

public class MonsterGenerator : AGenerator {

    public override DriftingItem Generate() {

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