
using UnityEngine;
using UnityEngine.Events;

public class UIButton : MonoBehaviour, ITouchable {

    public UnityEvent clickDownEvent;
    public UnityEvent clickUpEvent;
    UnityEngine.UI.Image image;
    bool clicked;
    int fingerId;

    private void Start() {
        image = GetComponent<UnityEngine.UI.Image>();
        clicked = false;
    }

    public void SetImage(Sprite sprite) {
        image.sprite = sprite;
    }

    public bool Touched(Touch touch) {
        if (touch.phase == TouchPhase.Began) {
            clicked = true;
            if (clickDownEvent != null)
                clickDownEvent.Invoke();
            fingerId = touch.fingerId;
        }
        else if (touch.fingerId != fingerId) {
            return false;
        }

        if (touch.phase == TouchPhase.Ended) {
            if (clickUpEvent != null && clicked) {
                clicked = false;
                clickUpEvent.Invoke();
            }
        }
        return true;
    }

}