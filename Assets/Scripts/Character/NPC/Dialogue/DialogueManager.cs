using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

namespace Character.NPC.Dialogue {

    [System.Serializable]
    public class ChoiceWrapper {
        [SerializeField] UnityEngine.UI.Button choiceButton;
        [SerializeField] TextMeshProUGUI choiceText;

        public UnityEngine.UI.Button Button {
            get => choiceButton;
        }

        public TextMeshProUGUI Text {
            get => choiceText;
        }

        public GameObject GameObject {
            get => choiceButton.gameObject;
        }
    }

    [System.Serializable]
    public class Profile {
        [SerializeField] UnityEngine.UI.Image mask;
        [SerializeField] UnityEngine.UI.Image profile;

        public Sprite Sprite {
            get => profile.sprite;
            set {
                float width = mask.rectTransform.sizeDelta.x;
                float height = width * value.rect.height / value.rect.width;
                profile.rectTransform.sizeDelta = new Vector2(width, height);
                profile.sprite = value;
            }
        }
    }

    public class DialogueManager : MonoBehaviour
    {
        private static DialogueManager _instance;
        public static DialogueManager Instance {
            get => _instance;
        }

        [SerializeField] GameObject dialoguePanel;
        [SerializeField] Profile profile;
        [SerializeField] TextMeshProUGUI dialogueText;
        [SerializeField] List<ChoiceWrapper> choices;
        [SerializeField] float gapTimeBetweenWord;

        Story curStory;
        string curLine;
        bool isShowingChoices;
        bool isShowingWords;

        private void Awake() {
            if (_instance == null)
                _instance = this;
        }

        private void Start() {
            ExitDialogue();
        }

        public void EnterDialogue(TextAsset inkJSON, Sprite sprite) {
            curStory = new Story(inkJSON.text);
            dialoguePanel.SetActive(true);
            profile.Sprite = sprite;
            AdvanceLine();
        }

        public void ExitDialogue() {
            isShowingChoices = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
            Gesture.GestureManager.Instance.ClearQueue();
            HideChoices();
        }

        public void AdvanceLine() {
            bool ended = true;
            if (isShowingWords) {
                StopAllCoroutines();
                isShowingWords = false;
                dialogueText.text = curLine;
                ended = false;
            }
            else if (curStory.canContinue) {
                curLine = curStory.Continue();
                StartCoroutine(ShowWords());
                ended = false;
            }
            else if (isShowingChoices || ShowChoices()) {
                ended = false;
            }

            if (ended) {
                ExitDialogue();
            }
            else {
                Gesture.GestureManager.Instance.Enqueue(NewTap());
            }
        }

        bool ShowChoices() {
            if (curStory.currentChoices.Count == 0)
                return false;
            isShowingChoices = true;
            for (int i = 0; i < curStory.currentChoices.Count; i++) {
                int j = i;
                Choice choice = curStory.currentChoices[i];
                choices[i].GameObject.SetActive(true);
                choices[i].Button.onClick.AddListener(delegate{ChooseChocie(j);});
                choices[i].Text.text = choice.text;
            }
            return true;
        }

        void HideChoices() {
            for (int i = 0; i < choices.Count; i++) {
                choices[i].Button.gameObject.SetActive(false);
                choices[i].Button.onClick.RemoveAllListeners();
                choices[i].Text.text = "";
            }
        }

        public void ChooseChocie(int i) {
            curStory.ChooseChoiceIndex(i);
            isShowingChoices = false;
            HideChoices();
            Gesture.GestureManager.Instance.Enqueue(NewTap());
        }

        System.Collections.IEnumerator ShowWords() {
            isShowingWords = true;
            string partial = "";
            foreach (char c in curLine) {
                partial += c;
                dialogueText.text = partial;
                yield return new WaitForSeconds(gapTimeBetweenWord);
            }
            isShowingWords = false;
        }

        Gesture.Tap NewTap() {
            Gesture.Tap tap = new Gesture.Tap();
            tap.TargetCount = 1;
            tap.SingleDuration = float.MaxValue;
            tap.OnSatisfied.AddListener(AdvanceLine);
            return tap;
        }
    }

}