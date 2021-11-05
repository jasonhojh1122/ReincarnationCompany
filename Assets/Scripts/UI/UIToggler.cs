using UnityEngine;
using System.Collections;
public class UIToggler : CustomButton {

    [SerializeField] CanvasGroupFader target;

    private void Start() {
        button.onClick.AddListener(target.Toggle);
    }

}