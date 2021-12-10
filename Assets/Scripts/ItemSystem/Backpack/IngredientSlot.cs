
using UnityEngine;
using TMPro;

public class IngredientSlot : ItemSlot {

    [SerializeField] GameObject itemCount;
    [SerializeField] TextMeshProUGUI itemCountText;
    static Color tint = new Color(0.37f, 0.37f, 0.37f, 1.0f);

    int count;


    public override void UpdateContent() {
        count = UserStateManager.Instance.Backpack.GetItemNum(itemData.itemName);
        if (count <= 0) {
            itemImage.color = tint;
        }
        else {
            itemImage.color = Color.white;
        }
        itemCountText.text = count.ToString();
    }

}