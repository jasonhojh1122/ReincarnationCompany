
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
    // public List<DriftingPatternData> driftingPatternDatas;

}

}