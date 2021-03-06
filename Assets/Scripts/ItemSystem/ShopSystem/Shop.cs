using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Shop {
public class Shop : MonoBehaviour {
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] CanvasGroupFader hint;
    [SerializeField] TextMeshProUGUI hintText;
    ShopItemData sid;

    int amount;

    void Awake() {
        amount = 1;
        UpdateText();
    }

    public void AddToCount(int i) {
        amount += i;
        amount = Mathf.Clamp(amount, 0, 10);
        UpdateText();
    }

    void UpdateText() {
        countText.text = amount.ToString();
    }

    public void Purchase() {
        if (UserStateManager.Instance.Money >= amount * sid.price) {
            UserStateManager.Instance.Money -= amount * sid.price;
            UserStateManager.Instance.Backpack.AddItemToBackpack(sid.baseData.name, amount);
            StartCoroutine(HintAnim());
        }
    }

    public void SetActiveItem(ShopItemData sid) {
        this.sid = sid;
        amount = 1;
        UpdateText();
    }

    System.Collections.IEnumerator HintAnim() {
        hintText.text = "已購買 " + sid.baseData.name + " x" + amount;
        hint.FadeIn();
        yield return new WaitForSeconds(0.6f);
        hint.FadeOut();
    }

}

}