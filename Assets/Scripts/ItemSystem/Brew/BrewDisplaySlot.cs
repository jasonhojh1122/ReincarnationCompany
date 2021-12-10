
using UnityEngine;
using UnityEngine.Events;

namespace Brew {

public class BrewDisplaySlot : ItemSlot {

    public UnityEvent OnReturn;

    static Color32 emptyColor = new Color32(0, 0, 0, 0);

    public override void Init(BaseItemData itemData) {
        this.itemData = null;
        button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(OnClick);
        itemImage.color = emptyColor;
    }

    public void SetItem(BaseItemData itemData) {
        this.itemData = itemData;
        itemImage.color = Color.white;
        itemImage.sprite = itemData.sprite;
    }

    public void ClearItem() {
        this.itemData = null;
        itemImage.color = emptyColor;
        itemImage.sprite = null;
    }

    public void OnClick() {
        if (itemData == null) return;
        ClearItem();
        OnReturn.Invoke();
    }

    public bool IsEmpty {
        get => itemData == null;
    }



}

}