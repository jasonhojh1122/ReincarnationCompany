using UnityEngine;
using TMPro;

public class NewCharacterText : MonoBehaviour {
    [SerializeField] ItemPool itemPool;
    [SerializeField] TextMeshPro characterName;
    [SerializeField] TextMeshPro characterDescription;
    private void Start() {
        characterName.text = "來世您將投胎成" + UserStateManager.Instance.CurCharacter;
        characterDescription.text = itemPool.characterPool[UserStateManager.Instance.CurCharacter].description;
    }
}