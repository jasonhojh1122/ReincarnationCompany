using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Ink.Runtime;

namespace Character.NPC {

    public class NPC : Character
    {
        [SerializeField] TextAsset dialogueJSON;
        [SerializeField] List<Dialogue.DialogueEvent> dialogueEvents;


        protected Story curStory;
        protected Dictionary<string, UnityEvent> eventMap;
        protected Animator animator;

        public Dictionary<string, UnityEvent> EventMap {
            get => eventMap;
        }

        public TextAsset DialogueJSON {
            get => dialogueJSON;
            set => dialogueJSON = value;
        }

        static Dialogue.DialogueManager dialogueManager;

        private new void Awake() {
            animator = GetComponent<Animator>();
            base.Awake();
        }

        protected void Start() {
            if (dialogueManager == null)
                dialogueManager = Dialogue.DialogueManager.Instance;
            eventMap = new Dictionary<string, UnityEvent>();
            foreach (Dialogue.DialogueEvent e in dialogueEvents) {
                eventMap.Add(e.Tag, e.OnChoice);
            }
        }

        public void Init(string mainCharacterName, string npcName) {
            UpdateCharacter(npcName);
            DialogueJSON = Utils.Loader.LoadDialogueData(mainCharacterName+"_"+npcName);
            InitAnimation();
            // DialogueJSON = Utils.Loader.Load<TextAsset>("DialogueData/" + characterName);
        }

        public virtual void Talk() {
            curStory = dialogueManager.EnterDialogue(this);
        }

        public void CountConversation() {
            UserStateManager.Instance.FinishedConversation[
                GameManager.Instance.ActivePlayer.CharacterData.baseData.itemName].
                ToHashSet().Add(characterData.baseData.itemName);
        }

        protected void InitAnimation() {
            if (animator == null) return;
            var controller = Utils.Loader.Load<RuntimeAnimatorController>(
                                characterData.baseData.itemName + '_' + MovingState.IDLE.ToString());
            animator.runtimeAnimatorController = controller;
            animator.Play(characterData.baseData.itemName + '_' + MovingState.IDLE.ToString(), 0, UnityEngine.Random.Range(0.0f, 1.0f));
        }

    }

}