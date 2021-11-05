
using UnityEngine;
using TMPro;

namespace Shop {
public class ShopSlot : ItemSlot {

    [SerializeField] TextMeshProUGUI priceText;
    public Shop shop;
    public int price;
    public ShopItemData shopItemData;

    public override void Init(BaseItemData itemData) {
        this.itemData = itemData;
        itemImage.sprite = itemData.sprite;
        button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(delegate{ itemViewer.Show(itemData); });
        button.onClick.AddListener(delegate{ shop.SetActiveItem(shopItemData); });
        UpdateContent();
    }

    public override void UpdateContent() {
        priceText.text = price.ToString();
    }

}

}