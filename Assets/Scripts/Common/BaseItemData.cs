
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ReincarnationCompany/BaseItemData", order = 0)]
public class BaseItemData : ScriptableObject {
    public string itemName;
    public string description;
    public Sprite sprite;
    public Animator animator;

}