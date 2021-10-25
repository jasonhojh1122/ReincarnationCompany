using UnityEngine;

public class UIToggler : UIButton {

    [SerializeField] GameObject target;
    [SerializeField] bool state;
    CanvasGroupFader canvasGroupFader;

    private void OnEnable() {
        canvasGroupFader = target.GetComponent<CanvasGroupFader>();
        // canvasGroupFader.OnTurnOff += DeactivateUI;
    }
    private void Start() {
        state = !state;
        ToggleUI();
    }

    public void ActivateUI() {
        target.SetActive(true);
    }

    public void DeactivateUI() {
        target.SetActive(false);
    }

    public void ToggleUI() {
        state = !state;
        if (state) {
            canvasGroupFader.gameObject.SetActive(true);
            canvasGroupFader.Toggle();
        }
        else
            canvasGroupFader.Toggle();
    }

}