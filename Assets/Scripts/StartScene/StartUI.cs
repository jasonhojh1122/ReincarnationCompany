using UnityEngine;

public class StartUI : MonoBehaviour {

    [SerializeField] UnityEngine.UI.Slider bgmSlider;
    [SerializeField] UnityEngine.UI.Slider soundEffectSlider;

    private void Start() {
        bgmSlider.onValueChanged.AddListener(OnBGMChange);
        soundEffectSlider.onValueChanged.AddListener(OnSEChange);
    }

    public void StartGame() {
        if (UserStateManager.Instance.IsNewGame) {
            GameManager.Instance.LoadSceneAndClose("_Select");
        }
        else {
            GameManager.Instance.LoadSceneAndClose("_Main");
        }
    }

    public void OnBGMChange(float value) {
        AudioManager.Instance.BgmVolume = value;
    }

    public void OnSEChange(float value) {
        AudioManager.Instance.SoundEffectVolume = value;
    }

    public void ResetData() {
        UserStateManager.Instance.ResetState();
    }

}