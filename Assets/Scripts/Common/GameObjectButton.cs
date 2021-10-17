using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectButton : MonoBehaviour, ITouchable
{
    public UnityEvent clickDownEvent;
    public UnityEvent clickUpEvent;

    public void Touched(Touch touch) {
        if (touch.phase == TouchPhase.Began && clickDownEvent != null) {
            clickDownEvent.Invoke();
        }
        else if (touch.phase == TouchPhase.Ended && clickUpEvent != null) {
            clickUpEvent.Invoke();
        }
    }
}

