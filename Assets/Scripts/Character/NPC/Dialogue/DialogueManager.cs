using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
                Utils.SpriteAndUI.FitSpriteToUIImage(mask.rectTransform, profile, value);
                /* float width = mask.rectTransform.sizeDelta.x;
                float height = width * value.rect.height / value.rect.width;
                profile.rectTransform.sizeDelta = new Vector2(width, height);
                profile.sprite = value; */
            }
        }
    }

    [System.Serializable]
    public struct DialogueEvent {
        public string Tag;
        public UnityEvent OnChoice;
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
        [SerializeField] public UnityEvent OnChoiceAppear;

        public List<ChoiceWrapper> Choices {
            get => choices;
        }

        NPC curNPC;
        Story curStory;
        string curLine;
        List<string> eventTags;
        bool isShowingWords;

        private void Awake() {
            if (_instance == null)
                _instance = this;
        }

        private void Start() {
            ExitDialogue();
        }

        public Story EnterDialogue(NPC npc) {
            ExitDialogue();
            curNPC = npc;
            curStory = new Story(npc.DialogueJSON.text);
            dialoguePanel.SetActive(true);
            if (npc.CharacterData != null)
                profile.Sprite = npc.CharacterData.baseData.sprite;
            OnTap();
            return curStory;
        }

        public void ExitDialogue() {
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
            Gesture.GestureManager.Instance.ClearQueue();
            HideChoices();
        }

        public void OnTap() {
            /* if (eventTags != null)
                CheckEvent(); */

            if (isShowingWords) {
                StopAllCoroutines();
                dialogueText.text = curLine;
                OnWordsEnd();
            }
            else if (curStory.canContinue) {
                ContinueDialogue();
            }
            else if (!curStory.canContinue) {
                ExitDialogue();
            }
        }

        void ContinueDialogue() {
            curLine = curStory.Continue();
            StartCoroutine(ShowWords());
            Gesture.GestureManager.Instance.Enqueue(NewTap());
        }

        void OnWordsEnd() {
            eventTags = curStory.currentTags;
            CheckEvent();
            Gesture.GestureManager.Instance.ClearQueue();
            isShowingWords = false;
            if (!curStory.canContinue && ShowChoices()) {
                return;
            }
            Gesture.GestureManager.Instance.Enqueue(NewTap());
        }

        System.Collections.IEnumerator ShowWords() {
            isShowingWords = true;
            dialogueText.text = "";
            foreach (char c in curLine) {
                dialogueText.text += c;
                yield return new WaitForSeconds(gapTimeBetweenWord);
            }
            OnWordsEnd();
        }

        bool ShowChoices() {
            if (curStory.currentChoices.Count == 0)
                return false;
            for (int i = 0; i < curStory.currentChoices.Count; i++) {
                int j = i;
                Choice choice = curStory.currentChoices[i];
                choices[i].GameObject.SetActive(true);
                choices[i].Button.onClick.AddListener(delegate{ChooseChoice(j);});
                choices[i].Text.text = choice.text;
            }
            OnChoiceAppear.Invoke();
            return true;
        }

        public void ChooseChoice(int i) {
            curStory.ChooseChoiceIndex(i);
            HideChoices();
            if (curStory.canContinue) {
                ContinueDialogue();
            }
        }

        void HideChoices() {
            for (int i = 0; i < choices.Count; i++) {
                choices[i].Button.gameObject.SetActive(false);
                choices[i].Button.onClick.RemoveAllListeners();
                choices[i].Text.text = "";
            }
        }

        Gesture.Tap NewTap() {
            Gesture.Tap tap = new Gesture.Tap();
            tap.TargetCount = 1;
            tap.SingleDuration = float.MaxValue;
            tap.OnSingleSatisfied.AddListener(OnTap);
            return tap;
        }

        void CheckEvent() {
            foreach (string tag in eventTags) {
                if (curNPC.EventMap.ContainsKey(tag)) {
                    curNPC.EventMap[tag].Invoke();
                }
            }
            eventTags = null;
        }
    }

}