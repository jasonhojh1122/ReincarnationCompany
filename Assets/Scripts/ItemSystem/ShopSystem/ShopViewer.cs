using UnityEngine;
using System.Collections.Generic;

namespace Shop {
public class ShopViewer : ItemViewer {

    [SerializeField] Shop shop;
    public override void InitContent()
    {
        foreach (ShopItemData sid in itemPool.shopItem) {
            ShopSlot slot = Instantiate<ItemSlot>(slotPrefab) as ShopSlot;
            slot.transform.SetParent(content, false);
            slot.price = sid.price;
            slot.shop = shop;
            slot.itemViewer = this;
            slot.shopItemData = sid;
            slot.Init(sid.baseData);
            slots.Add(slot);
            if (slots.Count == 1) {
                Show(sid.baseData);
                shop.SetActiveItem(sid);
            }
        }
    }

}

}