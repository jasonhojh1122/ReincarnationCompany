
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Brewer : MonoBehaviour {

    [SerializeField] List<Character.CharacterData> characterData;
    [SerializeField] Brew.BrewViewer brewViewer;
    [SerializeField] Character.Player player;

    string newCharacter;

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
        Debug.Log("Ingredient");
        foreach (KeyValuePair<string, int> p in ingredients) {
            Debug.Log(p.Key + " " + p.Value);
        }

        foreach (Character.CharacterData data in characterData) {
            Debug.Log(data.baseData.itemName);
            bool success = true;
            foreach (Character.Rule rule in data.rules) {
                success = RulePassed(rule, ingredients);
                if (!success) break;
            }
            if (success) {
                newCharacter = data.baseData.itemName;
                Reincarnate();
                break;
            }
        }
    }

    bool RulePassed(Character.Rule rule, Dictionary<string, int> ingredients) {

        if (rule.ruleState == Character.RuleState.INCLUDE) {
            if (rule.itemData == null) {
                Debug.Log(1);
                return (rule.minAmount <= ingredients.Count) && (rule.maxAmount >= ingredients.Count);
            }
            else if (!ingredients.ContainsKey(rule.itemData.itemName)
                    || ingredients[rule.itemData.itemName] < rule.minAmount
                    || ingredients[rule.itemData.itemName] > rule.maxAmount ) {
                Debug.Log(2);
                return false;
            }
            else {
                Debug.Log(3);
                return true;
            }
        }
        else {
            if (ingredients.ContainsKey(rule.itemData.itemName)) {
                Debug.Log(4);
                return false;
            }
            else {
                Debug.Log(5);
                return true;
            }
        }
    }

    void Reincarnate() {
        UserStateManager.Instance.UsedCharacter.Add(UserStateManager.Instance.CurCharacter);
        UserStateManager.Instance.NewCharacter = newCharacter;
        GameManager.Instance.LoadSceneAndClose("04-End-Video");
    }

}