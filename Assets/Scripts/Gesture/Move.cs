using UnityEngine;

namespace Gesture
{

    public class Move : AGesture
    {

        int fingerId;
        Vector2 startPos;
        float startTime;
        float movedOffset = 15f;
        Vector2 direction;
        float intervals;

        public Move() : base()
        {
            name = "Move";
        }

        public override void UpdateTouch(Touch touch)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    fingerId = touch.fingerId;
                    startTime = Time.time;
                    startPos = touch.position;
                    break;
                case TouchPhase.Moved://手按住滑動的狀態
                    direction = touch.position - startPos;
                    intervals = Time.realtimeSinceStartup - startTime;
                    break;
                case TouchPhase.Ended:
                    if ((touch.fingerId != fingerId) ||
                        direction.magnitude < movedOffset)
                    {
                        remainCount = targetCount;
                        indicator.UpdateCount(remainCount);
                        break;
                    }
                    remainCount--;
                    indicator.UpdateCount(remainCount);
                    break;
            }
        }

        public override bool IsSatisfied()
        {
            return remainCount == 0;
        }

        public override bool IsFailed()
        {
            return (Time.time - gestureStartTime > gestureData.singleDuration * targetCount);
        }


    }


}