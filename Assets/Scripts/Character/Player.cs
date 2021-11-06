
using UnityEngine;

public class Player : MonoBehaviour {

    public ItemPool itemPool;

    SpriteRenderer _renderer;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    public void UpdateSprite() {
        _renderer.sprite = itemPool.characterPool[UserStateManager.Instance.CurCharacter].sprite;
    }

}