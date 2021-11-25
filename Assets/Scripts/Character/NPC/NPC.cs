using UnityEngine;
using TMPro;
using System.Collections;

namespace Character.NPC {
    public class NPC : Character
    {
        [SerializeField] TextAsset dialogueJSON;

        public TextAsset DialogueJSON {
            get => dialogueJSON;
            set => dialogueJSON = value;
        }

        static Dialogue.DialogueManager dialogueManager;

        private void Start() {
            if (dialogueManager == null)
                dialogueManager = Dialogue.DialogueManager.Instance;
        }

        public void Init(string characterName) {
            UpdateCharacter(characterName);
            DialogueJSON = Utils.Loader.Load<TextAsset>("DialogueData/" + characterName);
        }

        public virtual void Talk() {
            Debug.Log("talk");
            dialogueManager.EnterDialogue(dialogueJSON, characterData.baseData.sprite);
        }

    }

}