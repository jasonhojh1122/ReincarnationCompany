using UnityEngine;

namespace CharacterSelection {

    public class SelectUI : MonoBehaviour {

        [SerializeField] GameObject content;
        [SerializeField] SelectionCard cardPrefab;

        SelectionCard activeCard;

        private void Start() {
            foreach (string characterName in UserStateManager.Instance.UsedCharacter) {
                Debug.Log(characterName);
                AddNewCard(characterName);
            }
        }

        void AddNewCard(string characterName) {
            var newCard = GameObject.Instantiate<SelectionCard>(cardPrefab);
            var characterData = Utils.Loader.LoadCharacterData(characterName);
            newCard.characterData = characterData;
            newCard.selectUI = this;
            newCard.Init();
            newCard.transform.SetParent(content.transform);
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