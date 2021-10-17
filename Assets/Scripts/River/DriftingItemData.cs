
using UnityEngine;
using System.Collections.Generic;

namespace River {

[CreateAssetMenu(fileName = "DriftingItemData", menuName = "ReincarnationCompany/DriftingItemData", order = 0)]
public class DriftingItemData : ScriptableObject {

    public enum Type {
        ENEMY,
        INGREDIENT,
        OTHER
    }

    public string itemName;
    public Sprite sprite;
    public Animator animator;
    public Type type;
    public float minSpeed;
    public float maxSpeed;

    [Header("Drifting Pattern")]
    public List<DriftingPatternData> driftingPatternDatas;
    [Range(1, 10)] public List<int> driftingPatternPriority;

    [Header("Gesture")]
    public List<Gesture.GestureData> gestureDatas;
    [Range(1, 10)] public List<int> gesturePriority;

    List<float> driftingPatternCDF = null;
    List<float> gestureCDF = null;

    private void OnEnable() {
        System.Diagnostics.Debug.Assert(driftingPatternDatas.Count == driftingPatternPriority.Count);
        System.Diagnostics.Debug.Assert(gestureDatas.Count == gesturePriority.Count);
        driftingPatternCDF = new List<float>();
        gestureCDF = new List<float>();
        CalculateCDF(driftingPatternDatas.Count, driftingPatternCDF, driftingPatternPriority);
        CalculateCDF(gestureDatas.Count, gestureCDF, gesturePriority);
    }

    private void CalculateCDF(int count, List<float> CDF, List<int> priority) {
        for (int i = 0; i < count; i++) {
            if (i == 0) {
                CDF.Add(priority[i]);
            }
            else {
                CDF.Add(priority[i] + CDF[i-1]);
            }
        }
        for (int i = 0; i < CDF.Count; i++) {
            CDF[i] /= CDF[CDF.Count-1];
        }
    }

    private int GetRandomIDFromCDF(List<float> CDF) {
        float rd = UnityEngine.Random.Range(0f, 1f);
        int id = CDF.BinarySearch(rd);
        if (id < 0) {
            id = ~id;
        }
        return id;
    }

    public DriftingPatternData GetRandomDriftingPattern() {
        int id = GetRandomIDFromCDF(driftingPatternCDF);
        return driftingPatternDatas[id];
    }

    public Gesture.GestureData GetRandomGesture() {
        int id = GetRandomIDFromCDF(gestureCDF);
        return gestureDatas[id];
    }

}

}