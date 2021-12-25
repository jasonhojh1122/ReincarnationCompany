
using UnityEngine;
using TMPro;

public class CharacterSlot : ItemSlot {

    static Color tint = new Color(0.05f, 0.05f, 0.05f, 1.0f);
    bool used;

    public override void UpdateContent() {
        used = UserStateManager.Instance.FinishedConversation.ContainsKey(itemData.itemName);
        if (!used) {
            itemImage.color = tint;
            button.onClick.RemoveAllListeners();
        }
        else {
            itemImage.color = Color.white;
        }
    }

}