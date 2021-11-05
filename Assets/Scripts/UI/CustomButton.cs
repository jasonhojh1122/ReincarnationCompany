using UnityEngine;

public class CustomButton : MonoBehaviour {

    protected GameManager gameManager;
    protected UnityEngine.UI.Button button;

    void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        button = GetComponent<UnityEngine.UI.Button>();
    }

}