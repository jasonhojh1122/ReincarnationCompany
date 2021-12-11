using UnityEngine;

namespace Character {
    public class EndingMovingTarget : MovingTarget {

        public override void Move(Vector2 joystickOffset) {
            if (paused) return;
            Vector2 vel = Vector2.zero;
            vel.x += speed * joystickOffset.x;

            Vector2 newPos = rb.position + vel * Time.deltaTime;
            if (!IsBlocked(newPos)) {
                rb.MovePosition(newPos);
                if (hasFacing) {
                    UpdateFacing(vel);
                }
            }
        }

        protected override void UpdateFacing(Vector2 vel) {
                if (Utils.Fuzzy.CloseVector2(vel, Vector2.zero)) {
                    animator.runtimeAnimatorController = animatorControllers[MovingState.IDLE];
                    return;
                }
                MovingState newState = MovingState.FRONT;
                if (vel.x > 0) newState = MovingState.RIGHT;
                else if (vel.x < 0) newState = MovingState.LEFT;
                if (animatorControllers.ContainsKey(newState))
                    animator.runtimeAnimatorController = animatorControllers[newState];
            }

    }
}