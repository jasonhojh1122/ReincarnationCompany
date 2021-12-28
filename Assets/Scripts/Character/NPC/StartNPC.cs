using UnityEngine;

namespace Character.NPC
{
    public class StartNPC : NPC {
        private new void Start() {
            base.Start();
            Talk();
        }

        public void End() {
            dialogueManager.ExitDialogue();
            GameManager.Instance.LoadSceneAndClose("_Main");
        }

    }
}
