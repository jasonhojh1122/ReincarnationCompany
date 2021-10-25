using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectButton : MonoBehaviour, ITouchable
{
    public UnityEvent clickDownEvent;
    public UnityEvent clickUpEvent;
    bool clicked;
    private void Start() {
        clicked = false;
    }

    public void Touched(Touch touch) {
        if (touch.phase == TouchPhase.Began) {
            clicked = true;
            if (clickDownEvent != null)
                clickDownEvent.Invoke();
        }
        else if (touch.phase == TouchPhase.Ended) {
            if (clickUpEvent != null && clicked) {
                clicked = false;
                clickUpEvent.Invoke();
            }
        }
    }
}

