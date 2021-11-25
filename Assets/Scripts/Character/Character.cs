
using UnityEngine;



namespace Character {
    public class Character : MonoBehaviour {

        protected CharacterData characterData;
        protected SpriteRenderer _renderer;
        protected BoxCollider2D col;

        public CharacterData CharacterData {
            get => characterData;
        }

        protected virtual void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            col = GetComponent<BoxCollider2D>();
        }

        public void UpdateCharacter(string characterName) {
            characterData = LoadData(characterName);
            _renderer.sprite = characterData.baseData.sprite;
            ResizeCollider();
        }

        protected static CharacterData LoadData(string characterName) {
            return Utils.Loader.Load<CharacterData>("CharacterData/" + characterName);
        }

        protected void ResizeCollider() {
            col.offset = Vector2.zero;
            col.size = new Vector2(_renderer.sprite.bounds.extents.x * transform.lossyScale.x,
                _renderer.sprite.bounds.extents.y * transform.lossyScale.y);
        }
    }
}