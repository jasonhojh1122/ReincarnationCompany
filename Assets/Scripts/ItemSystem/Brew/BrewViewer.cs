using UnityEngine;
using System.Collections.Generic;

namespace Brew {

public class BrewViewer : ItemViewer {

    [SerializeField] List<BrewDisplaySlot> displaySlots;
    [SerializeField] List<Transform> bookShelves;
    [SerializeField] Transform bookShelfPrefab;
    [SerializeField] IngredientSlot brewIngredientSlotPrefab;

    public List<BrewDisplaySlot> DispalySlots {
        get => displaySlots;
    }

    Dictionary<string, ItemSlot> slotMap;

    public override void InitContent() {
        // init display slots
        foreach (BrewDisplaySlot slot in displaySlots) {
            slot.Init(null);
            slot.itemViewer = this;
        }

        int i = 0;
        slotMap = new Dictionary<string, ItemSlot>();
        foreach (KeyValuePair<string, int> pair in UserStateManager.Instance.Backpack.backpackDict) {
            Debug.Log(i + " " + pair.Key);
            if (i >= 6 && i % 2 == 0) {
                Debug.Log(pair.Key);
                var bookShelf = GameObject.Instantiate<Transform>(bookShelfPrefab);
                bookShelf.SetParent(content, false);
                bookShelves.Add(bookShelf);
            }
            var slot = GameObject.Instantiate<IngredientSlot>(brewIngredientSlotPrefab);
            slot.itemViewer = this;
            slot.Init(itemPool.ingredientPool[pair.Key]);
            slot.transform.SetParent(bookShelves[i/2].transform, false);
            slotMap.Add(pair.Key, slot);
            i++;
        }
        UpdateContent();
    }

    public void ReturnItem(BaseItemData itemData) {
        UserStateManager.Instance.Backpack.AddItemToBackpack(itemData.itemName, 1);
        slotMap[itemData.itemName].UpdateContent();
    }

    public override void Show(BaseItemData itemData) {
        if (UserStateManager.Instance.Backpack.GetItemNum(itemData.itemName) <= 0)
            return;
        foreach (BrewDisplaySlot slot in displaySlots) {
            if (slot.IsEmpty) {
                slot.SetItem(itemData);
                slot.OnReturn.RemoveAllListeners();
                slot.OnReturn.AddListener(delegate{ReturnItem(itemData);});
                UserStateManager.Instance.Backpack.AddItemToBackpack(itemData.itemName, -1);
                slotMap[itemData.itemName].UpdateContent();
                break;
            }
        }
    }


}


}