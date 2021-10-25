
using UnityEngine;

namespace Shop {
public class ShopSlot : UIButton {

    Shop shop;
    ShopItemData shopItemData;

    public void Init(Shop shop, ShopItemData shopItemData) {
        this.shop = shop;
        this.shopItemData = shopItemData;
        clickUpEvent.AddListener(ShowInfo);
    }

    public void ShowInfo() {
        shop.ShowInfo(shopItemData);
    }

}

}