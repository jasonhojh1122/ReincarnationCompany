
using UnityEngine;

namespace Character {

    public class Player : Character {

        protected override void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            col = GetComponent<BoxCollider2D>();
            UpdateCharacter(UserStateManager.Instance.CurCharacter);
        }

    }

}