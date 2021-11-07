
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemPool", menuName = "ReincarnationCompany/ItemPool", order = 0)]
public class ItemPool : ScriptableObject {

    [SerializeField] public List<BaseItemData> ingredient;
    [SerializeField] public List<BaseItemData> monster;
    [SerializeField] public List<BaseItemData> character;
    [SerializeField] public List<Shop.ShopItemData> shopItem;

    public Dictionary<string, BaseItemData> ingredientPool;
    public Dictionary<string, BaseItemData> monsterPool;
    public Dictionary<string, BaseItemData> characterPool;
    public Dictionary<string, Shop.ShopItemData> shopPool;

    private void OnEnable() {
        ingredientPool = new Dictionary<string, BaseItemData>();
        monsterPool = new Dictionary<string, BaseItemData>();
        characterPool = new Dictionary<string, BaseItemData>();
        shopPool = new Dictionary<string, Shop.ShopItemData>();
        foreach (BaseItemData id in ingredient) {
            ingredientPool.Add(id.itemName, id);
        }
        foreach (BaseItemData id in monster) {
            monsterPool.Add(id.itemName, id);
        }
        foreach (BaseItemData id in character) {
            characterPool.Add(id.itemName, id);
        }
        foreach (Shop.ShopItemData id in shopItem) {
            shopPool.Add(id.baseData.name, id);
        }
    }
}