
using UnityEngine;
using System.Collections.Generic;

namespace River {

[CreateAssetMenu(fileName = "DriftingItemData", menuName = "ReincarnationCompany/DriftingItemData", order = 0)]
public class DriftingItemData : ScriptableObject {

    public BaseItemData baseData;
    public float minSpeed;
    public float maxSpeed;
    public int pikeUpPrice;

    [Header("Drifting Pattern")]
    public List<DriftingPatternData> driftingPatternDatas;
    public CDF driftingPatternCDF;

    [Header("Gesture")]
    public List<Gesture.GestureData> gestureDatas;
    public CDF gestureCDF;

    private void OnEnable() {
        driftingPatternCDF.CalculateCDF();
        gestureCDF.CalculateCDF();
    }

    public DriftingPatternData GetRandomDriftingPattern() {
        int id = driftingPatternCDF.GetRandomID();
        return driftingPatternDatas[id];
    }

    public Gesture.GestureData GetRandomGesture() {
        int id = gestureCDF.GetRandomID();
        return gestureDatas[id];
    }

}

}