
using UnityEngine;


namespace River {

[CreateAssetMenu(fileName = "DriftingItemData", menuName = "ReincarnationCompany/DriftingItemData", order = 0)]
public class DriftingItemData : ScriptableObject {

    public enum Type {
        ENEMY,
        INGREDIENT,
        OTHER
    }

    public Sprite sprite;
    public string itemName;
    public Animator animator;
    public Type type;
    public float minSpeed;
    public float maxSpeed;
    public float curSpeed;

}

}