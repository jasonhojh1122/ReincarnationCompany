using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Shop {
public class Shop : MonoBehaviour {

    [SerializeField] List<ShopItemData> shopItems;
    [SerializeField] Transform row;
    [SerializeField] TextMeshPro itemNameText;
    [SerializeField] TextMeshPro itemDescriptionText;
    [SerializeField] TextMeshPro itemPriceText;
    [SerializeField] TextMeshPro countText;
    int count;

    void Update() {
        countText.text = count.ToString();
    }

    public void AddToCount(int i) {
        count += i;
        count = Mathf.Clamp(i, 0, 10);
    }

    public void Purchase() {

    }

    public void ShowInfo(ShopItemData itemData) {
        count = 0;
        itemNameText.text = itemData.baseData.itemName;
        itemDescriptionText.text = itemData.baseData.description;
        itemPriceText.text = "$ " + itemData.price.ToString();
    }

}

}