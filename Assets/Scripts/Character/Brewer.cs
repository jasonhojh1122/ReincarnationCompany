
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Brewer : MonoBehaviour {

    [SerializeField] List<Character.CharacterData> characterData;
    [SerializeField] Brew.BrewViewer brewViewer;
    [SerializeField] CanvasGroupFader uiFader;
    [SerializeField] Player player;
    [SerializeField] TextMeshPro title;

    GameManager gameManager;

    string newCharacter;
    public string NewCharacter {
        get => newCharacter;
    }

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void BrewAndDrink() {
        Dictionary<string, int> ingredients = new Dictionary<string, int>();
        foreach (Brew.BrewDisplaySlot slot in brewViewer.DispalySlots) {
            if (slot.ItemData == null) continue;
            if (ingredients.ContainsKey(slot.ItemData.itemName)) {
                ingredients[slot.ItemData.itemName] += 1;
            }
            else {
                ingredients.Add(slot.ItemData.itemName, 1);
            }
        }

        foreach (Character.CharacterData data in characterData) {
            bool failed = false;
            foreach (Character.Rule rule in data.rules) {
                switch (rule.ruleState) {
                    case Character.RuleState.INCLUDE:
                        if (!ingredients.ContainsKey(rule.itemData.itemName)
                                || ingredients[rule.itemData.itemName] != rule.amount ) {
                            failed = true;
                        }
                        break;
                    case Character.RuleState.EXCLUDE:
                        if (ingredients.ContainsKey(rule.itemData.itemName)) {
                            failed = true;
                        }
                        break;
                }
                if (failed) break;
            }
            if (!failed) {
                newCharacter = data.baseData.itemName;
                uiFader.FadeOut();
                StartCoroutine(Reincarnate());
                break;
            }
        }
    }

    IEnumerator Reincarnate() {
        gameManager.ToggleUI(false);
        title.text = "你已經成功投胎為 " + newCharacter;
        UserStateManager.Instance.UsedCharacter.Add(UserStateManager.Instance.CurCharacter);
        UserStateManager.Instance.CurCharacter = newCharacter;
        player.UpdateSprite();
        Debug.Log(newCharacter);
        yield return null;
    }

    public void EndGame() {
        gameManager.EndGame();
    }

}