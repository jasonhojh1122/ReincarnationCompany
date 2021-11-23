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
    [SerializeField] Boat boat;

    [Header("Generators")]
    [SerializeField] List<Generator> generators;
    [SerializeField] CDF generatorCDF;

    GestureManager gestureManager;
    DriftingPatternPool driftingPatternTypePool;
    bool toGenerate;

    private void Awake() {
        gestureManager = FindObjectOfType<GestureManager>();
        generatorCDF.CalculateCDF();
    }

    void Start() {
        driftingPatternTypePool = new DriftingPatternPool();
        toGenerate = true;
        foreach (Generator g in generators) {
            g.SetDriftingPatternPool(driftingPatternTypePool);
            g.gestureManager = gestureManager;
        }
        StartCoroutine(KeepGenerate());
    }

    IEnumerator KeepGenerate() {
        while (true) {
            if (toGenerate) {
                Generate(generatorCDF.GetRandomID());
            }
            yield return Wait(UnityEngine.Random.Range(minInterval, maxInterval));
        }
    }

    IEnumerator Wait(float sec) {
        yield return new WaitForSeconds(sec);
    }

    public DriftingItem Generate(int id) {
        DriftingItem di = generators[id].Generate();
        Vector3 pos = new Vector3(transform.position.x, UnityEngine.Random.Range(minY, maxY), 0);
        di.transform.position = pos;
        di.transform.rotation = Quaternion.identity;
        di.Boat = boat;
        return di;
    }

}

}