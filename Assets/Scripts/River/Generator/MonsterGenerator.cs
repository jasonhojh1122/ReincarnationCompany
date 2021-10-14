using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace River {

public class MonsterGenerator : MonoBehaviour, IGenerator {
    [SerializeField] private List<DriftingItemData> pool;
    [SerializeField] private float minZ, maxZ, minSpeed, maxSpeed;
    [SerializeField] private DriftingItem prefab;
    List<ADriftingPattern> driftingPatterns;

    void Start()
    {
        driftingPatterns = new List<ADriftingPattern>();
        driftingPatterns.Add(new Straight(minZ, maxZ, minSpeed, maxSpeed));
        // Generate();
    }

    public DriftingItem Generate() {
        DriftingItem go = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        int dpID = UnityEngine.Random.Range(0, driftingPatterns.Count);
        System.Type type = driftingPatterns[dpID].GetType();
        ADriftingPattern pattern = (ADriftingPattern)System.Activator.CreateInstance(type,
            new System.Object[] {minZ, maxZ, minSpeed, maxSpeed});
        go.SetDriftingPattern(pattern);

        int itemID = UnityEngine.Random.Range(0, pool.Count);
        go.SetData(pool[itemID]);
        return go;
    }

}

}