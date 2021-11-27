
using UnityEngine;

namespace Character {

    [RequireComponent(typeof(MovingTarget))]
    public class Player : Character {

        MovingTarget movingTarget;
        protected static string[] stateName = System.Enum.GetNames(typeof(MovingState));

        protected override void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            col = GetComponent<BoxCollider2D>();
            movingTarget = GetComponent<MovingTarget>();
            UpdateCharacter(UserStateManager.Instance.CurCharacter);
        }

        private void Start() {
            SetupMovingTarget();
        }

        protected void SetupMovingTarget() {
            movingTarget.Speed = characterData.speed;
            foreach (string s in stateName) {
                var controller = Utils.Loader.Load<RuntimeAnimatorController>(
                                    characterData.baseData.itemName + '_' + s);
                var state = (MovingState) System.Enum.Parse(typeof(MovingState), s);
                movingTarget.AnimatorControllers.Add(state, controller);
            }
        }
    }

}