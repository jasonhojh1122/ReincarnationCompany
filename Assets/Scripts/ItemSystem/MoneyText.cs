using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour {
    [SerializeField] TextMeshProUGUI moneyText;

    private void Update() {
        moneyText.text = UserStateManager.Instance.Money.ToString();
    }
}