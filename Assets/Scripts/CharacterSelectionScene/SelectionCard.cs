
using UnityEngine;
using TMPro;

namespace CharacterSelection {

    public class SelectionCard : MonoBehaviour {

        [SerializeField] public GameObject selectedIndicator;
        [SerializeField] UnityEngine.UI.Image charaterImage;
        [SerializeField] TextMeshProUGUI characterDescription;
        [SerializeField] public UnityEngine.UI.Button button;

        public SelectUI selectUI;
        public Character.CharacterData characterData;


        private void Awake() {
            selectedIndicator.SetActive(false);
            button.onClick.AddListener(SelectCard);
        }

        public void Init() {
            Utils.SpriteAndUI.FitSpriteToUIImage(charaterImage.rectTransform, charaterImage, characterData.baseData.sprite);
            characterDescription.text = characterData.baseData.description;
        }

        private void SelectCard() {
            selectUI.SelectCard(this);
        }

    }

}