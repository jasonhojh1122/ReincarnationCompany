using UnityEngine;
using System.Collections.Generic;

namespace Brew {

public class BrewViewer : ItemViewer {

    [SerializeField] List<BrewDisplaySlot> displaySlots;
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

        slotMap = new Dictionary<string, ItemSlot>();
        foreach (KeyValuePair<string, int> pair in UserStateManager.Instance.Backpack.backpackDict) {
            Debug.Log(pair.Key + " " + pair.Value.ToString());
            ItemSlot slot = Instantiate<ItemSlot>(slotPrefab);
            slot.transform.SetParent(content, false);
            slot.itemViewer = this;
            slot.Init(itemPool.ingredientPool[pair.Key]);
            slots.Add(slot);
            slotMap.Add(pair.Key, slot);
        }
        UpdateContent();
    }

    public void ReturnItem(BaseItemData itemData) {
        UserStateManager.Instance.Backpack.AddItemToBackpack(itemData.itemName, 1);
        slotMap[itemData.itemName].UpdateContent();
    }

    public override void Show(BaseItemData itemData) {
        foreach (BrewDisplaySlot slot in displaySlots) {
            if (slot.IsEmpty) {
                if (UserStateManager.Instance.Backpack.GetItemNum(itemData.itemName) < 0)
                    return;
                slot.SetItem(itemData);
                slot.OnReturn.AddListener(delegate{ReturnItem(itemData);});
                UserStateManager.Instance.Backpack.AddItemToBackpack(itemData.itemName, -1);
                slotMap[itemData.itemName].UpdateContent();
                break;
            }
        }
    }


}


}