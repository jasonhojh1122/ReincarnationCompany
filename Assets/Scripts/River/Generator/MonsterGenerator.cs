using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace River {

public class MonsterGenerator : MonoBehaviour, IGenerator {
    [SerializeField] private DriftingItem prefab;
    [SerializeField] private List<DriftingItemData> monsterPool;
    public List<DriftingPatternData> driftingPatternDatas;
    Dictionary<string, System.Type> driftingPatternTypeMap;

    void Awake()
    {
        driftingPatternTypeMap = new Dictionary<string, System.Type>();
        System.Type dpt = typeof(ADriftingPattern);
        foreach (DriftingPatternData dpd in driftingPatternDatas) {
            string key = dpd.className;
            driftingPatternTypeMap.Add(key, System.Type.GetType(dpt.Namespace + '.' + key));
        }
    }

    public DriftingItem Generate() {
        DriftingItem go = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        int dpdID = UnityEngine.Random.Range(0, driftingPatternDatas.Count);
        System.Type type = driftingPatternTypeMap[driftingPatternDatas[dpdID].className];
        ADriftingPattern pattern = (ADriftingPattern)System.Activator.CreateInstance(type);
        pattern.Init(driftingPatternDatas[dpdID]);
        go.SetDriftingPattern(pattern);

        int itemID = UnityEngine.Random.Range(0, monsterPool.Count);
        go.SetData(monsterPool[itemID]);
        return go;
    }

}

}