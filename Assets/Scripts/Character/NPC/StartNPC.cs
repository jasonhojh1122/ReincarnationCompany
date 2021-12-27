using UnityEngine;

namespace Character.NPC
{
    public class StartNPC : NPC {
        private new void Start() {
            base.Start();
            Talk();
        }

        public void End() {
            GameManager.Instance.LoadSceneAndClose("_Main");
        }

    }
}
