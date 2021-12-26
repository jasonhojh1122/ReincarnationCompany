
using UnityEngine;
using System.Collections.Generic;

namespace Character {
    public enum MovingState {
        BACK,
        FRONT,
        LEFT,
        RIGHT,
        IDLE,
        PICK,
        DEFAULT
    }

    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class MovingTarget : MonoBehaviour {

        [SerializeField] protected float speed;
        [SerializeField] protected bool hasFacing = true;
        [SerializeField] int obstacleLayer = 8;

        protected Dictionary<MovingState, RuntimeAnimatorController> animatorControllers;
        protected Animator animator;
        protected BoxCollider2D col;
        protected Rigidbody2D rb;
        protected float blockThreshold;
        protected int mask;
        protected bool paused;

        public float Speed {
            get => speed;
            set => speed = value;
        }

        public bool Paused {
            get => paused;
            set => paused = value;
        }

        public bool HasFacing {
            get => hasFacing;
            set => hasFacing = value;
        }

        public Dictionary<MovingState, RuntimeAnimatorController> AnimatorControllers {
            get => animatorControllers;
        }

        static Dictionary<MovingState, Vector2> facingVec = new Dictionary<MovingState, Vector2> {
            {MovingState.BACK,  new Vector2(0, 1)},
            {MovingState.FRONT, new Vector2(0, -1)},
            {MovingState.LEFT,  new Vector2(-1, 0)},
            {MovingState.RIGHT, new Vector2(1, 0)}
        };

        private void Awake() {
            col = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            animatorControllers = new Dictionary<MovingState, RuntimeAnimatorController>();
            paused = false;
            mask = 1 << obstacleLayer;
        }

        private void Start() {
            blockThreshold = Mathf.Max(col.bounds.extents.x, col.bounds.extents.y) + 0.01f;
        }

        public virtual void Move(Vector2 joystickOffset) {
            if (paused) return;
            Vector2 vel = Vector2.zero;
            vel.x += speed * joystickOffset.x;
            vel.y += speed * joystickOffset.y;

            Vector2 newPos = rb.position + vel * Time.deltaTime;
            if (!IsBlocked(newPos)) {
                rb.MovePosition(newPos);
                if (hasFacing) {
                    UpdateFacing(vel);
                }
            }
        }

        protected virtual bool IsBlocked(Vector2 newPos) {
            Vector2 a = new Vector2(newPos.x - col.bounds.extents.x,
                                    newPos.y + col.bounds.extents.y);
            Vector2 b = new Vector2(newPos.x + col.bounds.extents.x,
                                    newPos.y - col.bounds.extents.y);
            Collider2D[] colliders = Physics2D.OverlapAreaAll(a, b, mask);
            return colliders.Length > 0;
        }

        protected virtual void UpdateFacing(Vector2 vel) {
            if (Utils.Fuzzy.CloseVector2(vel, Vector2.zero)) {
                animator.runtimeAnimatorController = animatorControllers[MovingState.IDLE];
                return;
            }
            MovingState newState = MovingState.FRONT;
            float maxDot = float.MinValue;
            foreach (KeyValuePair<MovingState, Vector2> p in facingVec) {
                float dot = Vector2.Dot(vel, p.Value);
                if (dot > maxDot) {
                    maxDot = dot;
                    newState = p.Key;
                }
            }
            if (animatorControllers.ContainsKey(newState))
                animator.runtimeAnimatorController = animatorControllers[newState];
        }

        public virtual void SetState(MovingState state) {
            if (state == MovingState.DEFAULT)
                animator.runtimeAnimatorController = null;
            else
                animator.runtimeAnimatorController = animatorControllers[state];
        }

    }
}