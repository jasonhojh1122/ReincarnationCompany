using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class GameObjectButton : MonoBehaviour, ITouchable
{
    public UnityEvent clickDownEvent;
    public UnityEvent clickUpEvent;
    int fingerId;
    bool clicked;
    private void Start() {
        clicked = false;
    }

    public bool Touched(Touch touch) {
        if (touch.phase == TouchPhase.Began) {
            fingerId = touch.fingerId;
            clicked = true;
            if (clickDownEvent != null)
                clickDownEvent.Invoke();
            return true;
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

