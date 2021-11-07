
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ReincarnationCompany/BaseItemData", order = 0)]
public class BaseItemData : ScriptableObject {
    public string itemName;
    [TextArea(4,12)] public string description;
    public Sprite sprite;
    public Animator animator;

}