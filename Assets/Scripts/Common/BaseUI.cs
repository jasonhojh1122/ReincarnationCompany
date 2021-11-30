
using UnityEngine;

public class BaseUI : MonoBehaviour {

    [SerializeField] CanvasGroupFader joystick;
    [SerializeField] CanvasGroupFader backpack;
    [SerializeField] CanvasGroupFader money;
    [SerializeField] CanvasGroupFader map;

    public void Reset() {
        joystick.FadeIn();
        backpack.FadeIn();
        money.FadeIn();
        // map.FadeIn();
    }

    public void Set(SceneSetting sceneSetting) {
        if (!sceneSetting.showJoyStick) {
            joystick.FadeOut();
        }
        if (!sceneSetting.showBackpack) {
            backpack.FadeOut();
        }
        if (!sceneSetting.showMoney) {
            money.FadeOut();
        }
        if (!sceneSetting.showMap) {
            // map.FadeOut();
        }
    }


}