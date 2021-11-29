
using UnityEngine;

namespace Character {

    public class Character : MonoBehaviour {

        [SerializeField] protected CharacterData characterData;
        protected SpriteRenderer _renderer;
        protected BoxCollider2D col;

        public CharacterData CharacterData {
            get => characterData;
        }

        protected virtual void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            col = GetComponent<BoxCollider2D>();
            if (characterData != null)
                UpdateCharacter(characterData.baseData.itemName);
        }

        public void UpdateCharacter(string characterName) {
            characterData = LoadData(characterName);
            _renderer.sprite = characterData.baseData.sprite;
            Utils.Fuzzy.MatchBoxColliderToSprite(col, _renderer.sprite);
        }

        protected static CharacterData LoadData(string characterName) {
            return Utils.Loader.Load<CharacterData>("CharacterData/" + characterName);
        }
    }
}