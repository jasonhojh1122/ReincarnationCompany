
using UnityEngine;
using TMPro;

public class ItemSlot : MonoBehaviour {
    [SerializeField] protected UnityEngine.UI.Image itemImage;
    public ItemViewer itemViewer;

    UnityEngine.UI.Button button;

    protected BaseItemData itemData;

    public virtual void Init(BaseItemData itemData) {
        this.itemData = itemData;
        itemImage.sprite = itemData.sprite;
        button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(delegate{ itemViewer.Show(itemData); });
        UpdateContent();
    }

    public virtual void UpdateContent() {
        //
    }



}