

using UnityEngine;
using System.Collections.Generic;

public class CharacterViewer : ItemViewer {

    public override void InitContent()
    {
        foreach (BaseItemData bid in itemPool.character) {
            ItemSlot slot = Instantiate<ItemSlot>(slotPrefab);
            slot.transform.SetParent(content, false);
            slot.Init(bid);
            slot.itemViewer = this;
            slots.Add(slot);
        }
    }


}