
using UnityEngine;

public class ActivatePopup : ActivateZone {

    [SerializeField] GameObject target;

    private void Start() {
        Hide();
        onEnter.AddListener(Show);
        onExit.AddListener(Hide);
    }

    protected void Show() {
        target.SetActive(true);
    }

    protected void Hide() {
        target.SetActive(false);
    }

}