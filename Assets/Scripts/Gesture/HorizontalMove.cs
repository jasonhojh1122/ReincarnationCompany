using UnityEngine;

namespace Gesture {

public class HorizontalMove : AGesture {

    int fingerId;
    Vector2 startPos;
    Vector2 lastPos;
    float tapStartTime;
    float movedOffset = Screen.width / 4.0f;
    bool newMovement;
    bool rightMovement;

    public HorizontalMove() : base() {
        name = "HorizontalMove";
        fingerId = -1;
    }

    public override void UpdateTouch(Touch touch) {
        switch(touch.phase) {
            case TouchPhase.Began:
                if (fingerId == -1) {
                    fingerId = touch.fingerId;
                    tapStartTime = Time.time;
                    lastPos = startPos = touch.position;
                    newMovement = true;
                }
                break;
            case TouchPhase.Moved:
                if (touch.fingerId == fingerId) {
                    CheckNewPos(touch);
                }
                break;
            case TouchPhase.Ended:
                if (touch.fingerId == fingerId) {
                    lastPos = touch.position;
                    CheckSingleSatisfied();
                    fingerId = -1;
                }
                break;
        }
    }

    void CheckNewPos(Touch touch) {
        if (newMovement) {
            newMovement = false;
            if (touch.position.x > startPos.x) {
                rightMovement = true;
            }
            else {
                rightMovement = false;
            }
        }
        else if (rightMovement) {
            if (touch.position.x < lastPos.x) {
                rightMovement = false;
                CheckSingleSatisfied();
                startPos = touch.position;
            }
        }
        else {
            if (touch.position.x > lastPos.x) {
                rightMovement = true;
                CheckSingleSatisfied();
                startPos = touch.position;
            }
        }
        lastPos = touch.position;

    }

    void CheckSingleSatisfied() {
        if (Mathf.Abs(lastPos.x - startPos.x) >= movedOffset) {
            remainCount--;
            OnSingleSatisfied.Invoke();
        }
        else {
            startPos = lastPos;
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