using UnityEngine;

namespace Gesture {

public class Tap : AGesture {

    int fingerId;
    Vector2 startPos;
    float tapStartTime;
    float movedOffset = 15f;

    public Tap() : base() {
        name = "Tap";
    }

    public override void UpdateTouch(Touch touch) {
        switch(touch.phase) {
            case TouchPhase.Began:
                fingerId = touch.fingerId;
                tapStartTime = Time.time;
                startPos = touch.position;
                break;
            case TouchPhase.Ended:
                if ((touch.fingerId != fingerId) ||
                    ((touch.position - startPos).magnitude > movedOffset) ) {
                    break;
                }
                remainCount--;
                OnSingleSatisfied.Invoke();
                break;
        }
    }

    public override bool IsSatisfied() {
        return remainCount == 0;
    }

    public override bool IsFailed() {
        return (Time.time - gestureStartTime > singleDuration * targetCount);
    }


}


}