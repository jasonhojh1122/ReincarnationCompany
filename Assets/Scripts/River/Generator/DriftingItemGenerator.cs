using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Gesture;

namespace River {

public class DriftingItemGenerator : MonoBehaviour {

    [Header("Basic Settings")]
    [SerializeField] float minInterval;
    [SerializeField] float maxInterval;
    [SerializeField] float minY;
    [SerializeField] float maxY;

/*     [Header("Monster Settings")]
    [SerializeField] private DriftingItem monsterPrefab;
    [SerializeField] private List<DriftingItemData> monsterPool;

    [Header("Ingredient Settings")]
    [SerializeField] private DriftingItem ingredientPrefab;
    [SerializeField] private List<DriftingItemData> ingredientPool;
 */
    [Header("Generators")]
    [SerializeField] List<AGenerator> generators;

    DriftingPatternPool driftingPatternTypePool;
    GesturePool gesturePool;
    bool toGenerate;

    void Start() {
        driftingPatternTypePool = new DriftingPatternPool();
        gesturePool = new GesturePool();
        toGenerate = true;
        foreach (AGenerator g in generators) {
            g.SetDriftingPatternPool(driftingPatternTypePool);
            g.SetGesturePool(gesturePool);
        }
        StartCoroutine(KeepGenerate());
    }

    IEnumerator KeepGenerate() {
        while (true) {
            if (toGenerate) {
                Generate();
            }
            yield return Wait(UnityEngine.Random.Range(minInterval, maxInterval));
        }
    }

    IEnumerator Wait(float sec) {
        yield return new WaitForSeconds(sec);
    }

    public DriftingItem Generate() {
        DriftingItem di = generators[0].Generate();
        Vector3 pos = new Vector3(transform.position.x, UnityEngine.Random.Range(minY, maxY), 0);
        di.transform.position = pos;
        di.transform.rotation = Quaternion.identity;
        return di;
    }

}

}