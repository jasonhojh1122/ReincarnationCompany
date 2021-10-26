
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemPool", menuName = "ReincarnationCompany/ItemPool", order = 0)]
public class ItemPool : ScriptableObject {

    [SerializeField] public List<BaseItemData> ingredient;
    [SerializeField] public List<BaseItemData> monster;
    [SerializeField] public List<BaseItemData> character;

    public Dictionary<string, BaseItemData> ingredientPool;
    public Dictionary<string, BaseItemData> monsterPool;
    public Dictionary<string, BaseItemData> characterPool;

    private void Start() {
        ingredientPool = new Dictionary<string, BaseItemData>();
        monsterPool = new Dictionary<string, BaseItemData>();
        characterPool = new Dictionary<string, BaseItemData>();
        foreach (BaseItemData id in ingredient) {
            ingredientPool.Add(id.itemName, id);
        }
        foreach (BaseItemData id in monster) {
            monsterPool.Add(id.itemName, id);
        }
        foreach (BaseItemData id in character) {
            characterPool.Add(id.itemName, id);
        }
    }
}