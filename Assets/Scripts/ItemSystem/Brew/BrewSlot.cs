
using UnityEngine;
using TMPro;

namespace Brew {

public class BrewSlot : ItemSlot {

    [SerializeField] GameObject itemCount;
    [SerializeField] TextMeshProUGUI itemCountText;
    static Color tint = new Color(95, 95, 95, 255);

    int count;


    public override void UpdateContent() {
        count = UserStateManager.Instance.Backpack.GetItemNum(itemData.itemName);
        if (count == 0) {
            itemImage.color = tint;
        }
        else {
            itemImage.color = Color.white;
        }
        itemCountText.text = count.ToString();
    }

}

}