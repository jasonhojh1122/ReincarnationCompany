
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
    public float pickUpPrice;
    public float minSpeed;
    public float maxSpeed;

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