using UnityEngine;
using System.Collections.Generic;
using TMPro;

public abstract class ItemViewer : MonoBehaviour {
    [SerializeField] protected ItemPool itemPool;
    [SerializeField] protected ItemSlot slotPrefab;
    [SerializeField] protected Transform content;
    [SerializeField] protected UnityEngine.UI.Image imageDisplay;
    [SerializeField] protected TextMeshProUGUI nameTextDisplay;
    [SerializeField] protected TextMeshProUGUI descriptionTextDisplay;

    Vector2 imageDisplayDelta;

    protected List<ItemSlot> slots;

    private void Awake() {
        slots = new List<ItemSlot>();
        if (imageDisplay != null)
            imageDisplayDelta = imageDisplay.rectTransform.sizeDelta;
        InitContent();
    }

    public abstract void InitContent();

    public virtual void UpdateContent() {
        foreach (ItemSlot slot in slots) {
            slot.UpdateContent();
        }
    }

    public virtual void Show(BaseItemData itemData) {
        // imageDisplay.sprite = itemData.sprite;
        imageDisplay.rectTransform.sizeDelta = imageDisplayDelta;
        Utils.SpriteAndUI.FitSpriteToUIImage(imageDisplay.rectTransform, imageDisplay, itemData.sprite);
        nameTextDisplay.text = itemData.itemName;
        descriptionTextDisplay.text = itemData.description;
    }

    public void ShowFirst() {
        if (slots[0] != null)
            Show(slots[0].ItemData);
    }

}
