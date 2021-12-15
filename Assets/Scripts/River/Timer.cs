
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
    [SerializeField] float gameTime = 60.0f;
    [SerializeField] TextMeshProUGUI timeText;

    bool stopped;

    float GameTime {
        get => gameTime;
        set {
            gameTime = value;
            timeText.text = gameTime.ToString("0.00");
        }
    }

    private void Awake() {
        stopped = false;
    }

    private void Update() {
        if (stopped) return;
        GameTime -= Time.deltaTime;
        if (GameTime <= 0.0f) {
            stopped = true;
            GameManager.Instance.LoadSceneAndClose("01-River-End");
        }
    }
}