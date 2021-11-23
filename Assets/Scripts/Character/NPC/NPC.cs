using UnityEngine;
using TMPro;
using System.Collections;

namespace Character {
    public class NPC : Character
    {
        [SerializeField] Transform textBlock;
        [SerializeField] TextMeshPro lineText;
        DialogueData dialogueData;

        bool finishedTalking;
        int curLine;

        static Gesture.GestureManager gestureManager;

        private void Start() {
            finishedTalking = false;
            textBlock.gameObject.SetActive(false);
            if (gestureManager == null)
                gestureManager = FindObjectOfType<Gesture.GestureManager>();
        }

        public void Init(string characterName) {
            UpdateCharacter(characterName);
            dialogueData = Utils.Loader.Load<DialogueData>("DialogueData/" + characterName);
        }


        // on player enter activate zone
        public void StartDialogue() {
            if (finishedTalking) return;
            textBlock.gameObject.SetActive(true);
            curLine = -1;
            finishedTalking = false;
            gestureManager.Enqueue(NewTap());
            AdvanceLine();
        }

        public void AdvanceLine() {
            curLine += 1;
            if (curLine >= dialogueData.lines.Count) {
                finishedTalking = true;
                textBlock.gameObject.SetActive(false);
                return;
            }
            else {
                lineText.text = dialogueData.lines[curLine];
                gestureManager.Enqueue(NewTap());
            }
        }

        // on player exit activate zone
        public void EndDialogue() {
            finishedTalking = false;
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