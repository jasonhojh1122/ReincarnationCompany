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

        public Dictionary<string, UnityEvent> EventMap {
            get => eventMap;
        }

        public TextAsset DialogueJSON {
            get => dialogueJSON;
            set => dialogueJSON = value;
        }

        static Dialogue.DialogueManager dialogueManager;

        private void Start() {
            if (dialogueManager == null)
                dialogueManager = Dialogue.DialogueManager.Instance;
            eventMap = new Dictionary<string, UnityEvent>();
            foreach (Dialogue.DialogueEvent e in dialogueEvents) {
                eventMap.Add(e.Tag, e.OnChoice);
            }
        }

        public void Init(string characterName) {
            UpdateCharacter(characterName);
            DialogueJSON = Utils.Loader.Load<TextAsset>("DialogueData/" + characterName);
        }

        public virtual void Talk() {
            curStory = dialogueManager.EnterDialogue(this);
        }

    }

}