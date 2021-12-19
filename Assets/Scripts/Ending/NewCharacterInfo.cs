using UnityEngine;
using TMPro;

public class NewCharacterInfo : MonoBehaviour {
    [SerializeField] SpriteRenderer spriteRenderer;
    private void Start() {
        var data = Utils.Loader.LoadCharacterData(UserStateManager.Instance.CurCharacter);
        spriteRenderer.sprite = data.artWords;
    }
}