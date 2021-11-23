
using UnityEngine;

public class Sign : MonoBehaviour {

    [SerializeField] GameObject popup;

    private void Start() {
        popup.SetActive(false);
    }

    public void Popup() {
        popup.SetActive(true);
    }

    public void UnPopup() {
        popup.SetActive(false);
    }

}