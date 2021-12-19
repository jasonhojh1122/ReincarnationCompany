using UnityEngine;
using System.Collections.Generic;

namespace CharacterSelection {

    public class SelectUI : MonoBehaviour {

        [SerializeField] GameObject content;
        [SerializeField] SelectionCard cardPrefab;

        SelectionCard activeCard;

        private void Start() {
            foreach (KeyValuePair<string, SerializableHashSet<string>> pair in UserStateManager.Instance.FinishedConversation) {
                var characterData = Utils.Loader.LoadCharacterData(pair.Key);
                if (characterData.withStory)
                    AddNewCard(pair.Key, characterData);
            }
        }

        void AddNewCard(string characterName, Character.CharacterData characterData) {
            var newCard = GameObject.Instantiate<SelectionCard>(cardPrefab);
            newCard.characterData = characterData;
            newCard.selectUI = this;
            newCard.Init();
            newCard.transform.SetParent(content.transform, false);
        }

        public void SelectCard(SelectionCard selectionCard) {
            if (activeCard != null) {
                activeCard.selectedIndicator.SetActive(false);
            }
            activeCard = selectionCard;
            activeCard.selectedIndicator.SetActive(true);
        }

        public void Confirm() {
            UserStateManager.Instance.CurCharacter = activeCard.characterData.baseData.itemName;
            UserStateManager.Instance.Money = activeCard.characterData.defaultMoney;
            UserStateManager.Instance.Backpack.ClearBackpack();
            UserStateManager.Instance.IsNewGame = false;
            GameManager.Instance.LoadSceneAndClose("_Main");
        }

    }

}