
using UnityEngine;
using TMPro;

namespace CharacterSelection {

    public class SelectionCard : MonoBehaviour {

        [SerializeField] public GameObject selectedIndicator;
        [SerializeField] UnityEngine.UI.Image charaterImage;
        [SerializeField] TextMeshProUGUI characterName;
        [SerializeField] TextMeshProUGUI characterDescription;
        [SerializeField] TextMeshProUGUI conversationText;
        [SerializeField] public UnityEngine.UI.Button button;

        public SelectUI selectUI;
        public Character.CharacterData characterData;


        private void Awake() {
            selectedIndicator.SetActive(false);
            button.onClick.AddListener(SelectCard);
        }

        public void Init() {
            Utils.SpriteAndUI.FitSpriteToUIImage(charaterImage.rectTransform, charaterImage, characterData.baseData.sprite);
            characterName.text = characterData.baseData.itemName;
            characterDescription.text = characterData.baseData.description;
            conversationText.text = "故事完成 " +
                UserStateManager.Instance.FinishedConversation[characterData.baseData.itemName].ToHashSet().Count.ToString() +
                "/" + characterData.NPCs.Count.ToString();
        }

        private void SelectCard() {
            selectUI.SelectCard(this);
        }

    }

}