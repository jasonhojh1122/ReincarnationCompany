using UnityEngine;

namespace Gesture {

public class VerticalMove : AGesture {

    int fingerId;
    Vector2 startPos;
    Vector2 lastPos;
    float tapStartTime;
    float movedOffset = Screen.height / 4.0f;
    bool newMovement;
    bool upMovement;

    public VerticalMove() : base() {
        name = "VerticalMove";
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
            if (touch.position.y > startPos.y) {
                upMovement = true;
            }
            else {
                upMovement = false;
            }
        }
        else if (upMovement) {
            if (touch.position.y < lastPos.y) {
                upMovement = false;
                CheckSingleSatisfied();
                startPos = touch.position;
            }
        }
        else {
            if (touch.position.y > lastPos.y) {
                upMovement = true;
                CheckSingleSatisfied();
                startPos = touch.position;
            }
        }
        lastPos = touch.position;

    }

    void CheckSingleSatisfied() {
        if (Mathf.Abs(lastPos.y - startPos.y) >= movedOffset) {
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