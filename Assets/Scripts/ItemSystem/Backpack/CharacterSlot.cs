
using UnityEngine;
using TMPro;

public class CharacterSlot : ItemSlot {

    static Color tint = new Color(95, 95, 95, 255);
    bool used;

    public override void UpdateContent() {
        used = UserStateManager.Instance.FinishedConversation.ContainsKey(itemData.itemName);
        if (used) {
            itemImage.color = tint;
        }
        else {
            itemImage.color = Color.white;
        }
    }

}