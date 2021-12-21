using UnityEngine;
using TMPro;

public class NewCharacterInfo : MonoBehaviour {
    [SerializeField] TextMeshPro characterName;
    [SerializeField] TextMeshPro characterDescription;
    // [SerializeField] SpriteRenderer spriteRenderer;
    private void Start() {
        var data = Utils.Loader.LoadCharacterData(UserStateManager.Instance.CurCharacter);
        characterName.text = "〈" + data.baseData.itemName + "〉";
        characterDescription.text = data.baseData.description;
        //spriteRenderer.sprite = data.artWords;
    }
}