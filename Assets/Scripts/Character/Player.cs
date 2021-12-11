
using UnityEngine;

namespace Character {

    [RequireComponent(typeof(MovingTarget))]
    public class Player : Character {

        protected MovingTarget movingTarget;
        protected static string[] stateName = System.Enum.GetNames(typeof(MovingState));

        public MovingTarget MovingTarget {
            get => movingTarget;
        }

        protected override void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            col = GetComponent<BoxCollider2D>();
            movingTarget = GetComponent<MovingTarget>();
            UpdateCharacter(UserStateManager.Instance.CurCharacter);
        }

        protected virtual void Start() {
            SetupMovingTarget();
        }

        protected void SetupMovingTarget() {
            movingTarget.Speed = characterData.speed;
            if (!movingTarget.HasFacing) return;
            foreach (string s in stateName) {
                var controller = Utils.Loader.Load<RuntimeAnimatorController>(
                                    characterData.baseData.itemName + '_' + s);
                var state = (MovingState) System.Enum.Parse(typeof(MovingState), s);
                movingTarget.AnimatorControllers.Add(state, controller);
            }
        }
    }

}