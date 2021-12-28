using UnityEngine;

public class SceneSetting : MonoBehaviour {

    public bool showJoyStick;
    public Character.Player player;

    public bool showBackpack;

    public bool showMap;

    public bool showMoney;

    public AudioClip bgm;

    public Canvas canvas;

    public static SceneSetting activeSceneSetting;

    public UnityEngine.Video.VideoPlayer videoPlayer;

    private void Awake() {
        activeSceneSetting = this;
    }

}