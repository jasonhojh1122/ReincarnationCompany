
using UnityEngine;

public class ReturnButton : CustomButton {

    void Start() {
        button.onClick.AddListener(gameManager.UnloadScene);
    }


}