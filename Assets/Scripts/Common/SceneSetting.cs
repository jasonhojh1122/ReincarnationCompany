using UnityEngine;

public class SceneSetting : MonoBehaviour {

    public bool showJoyStick;
    public Character.Player player;

    public bool showBackpack;

    public bool showMap;

    public bool showMoney;

    public Canvas canvas;

    public static SceneSetting activeSceneSetting;

    private void Awake() {
        activeSceneSetting = this;
    }

}