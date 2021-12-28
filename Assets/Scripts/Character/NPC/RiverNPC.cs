
using UnityEngine;

namespace Character.NPC {
    public class RiverNPC : NPC {
        [SerializeField] int price = 100;
        public void CheckMoney() {
            if (UserStateManager.Instance.Money < price) {
                curStory.ChoosePathString("RunOutOfMoney");
            }
            else {
                curStory.ChoosePathString("EnoughMoney");
            }
        }

        public void Enter() {
            UserStateManager.Instance.Money -= price;
            Dialogue.DialogueManager.Instance.ExitDialogue();
            if (UserStateManager.Instance.FinishedConversation.Count == 1)
                GameManager.Instance.LoadSceneAndClose("01-River-Tutorial");
            else
                GameManager.Instance.LoadSceneAndClose("01-River");
        }


    }

}