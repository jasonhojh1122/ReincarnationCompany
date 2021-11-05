using UnityEngine;

namespace Shop {

[CreateAssetMenu(fileName = "ItemData", menuName = "ReincarnationCompany/ShopItemData", order = 0)]
public class ShopItemData : ScriptableObject {
    public BaseItemData baseData;
    public int price;
}

}