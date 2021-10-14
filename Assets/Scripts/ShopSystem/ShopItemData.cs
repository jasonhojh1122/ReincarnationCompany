using UnityEngine;


[CreateAssetMenu(fileName = "ItemData", menuName = "ReincarnationCompany/ShopItemData", order = 0)]
public class ShopItemData : ScriptableObject {
    public Sprite sprite;
    public string itemName;
    public string description;
    public Animator animator;
}