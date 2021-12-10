
using UnityEngine;
using System.Collections.Generic;

namespace Character.NPC {
    public class RiverFailNPC : NPC {

        public void CutMoney()
        {
            UserStateManager.Instance.Money -= 100;
            Back();
        }

        public void Back()
        {
            Dialogue.DialogueManager.Instance.ExitDialogue();
            GameManager.Instance.UnloadScene();
        }

    }

}