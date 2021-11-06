
using UnityEngine;
using UnityEngine.Events;

namespace Brew {

public class BrewDisplaySlot : ItemSlot {

    public UnityEvent OnReturn;

    public override void Init(BaseItemData itemData) {
        this.itemData = null;
        button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(OnClick);
    }

    public void SetItem(BaseItemData itemData) {
        this.itemData = itemData;
        itemImage.sprite = itemData.sprite;
    }

    public void ClearItem() {
        this.itemData = null;
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