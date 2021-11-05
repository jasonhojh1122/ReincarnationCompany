

using UnityEngine;
using System.Collections.Generic;

public class IngredientViewer : ItemViewer {

    public override void InitContent()
    {
        foreach (BaseItemData bid in itemPool.ingredient) {
            ItemSlot slot = Instantiate<ItemSlot>(slotPrefab);
            slot.transform.SetParent(content, false);
            slot.Init(bid);
            slot.itemViewer = this;
            slots.Add(slot);
            if (slots.Count == 1) {
                Show(bid);
            }
        }
    }


}