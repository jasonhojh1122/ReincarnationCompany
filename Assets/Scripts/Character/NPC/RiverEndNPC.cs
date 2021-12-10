
using UnityEngine;
using System.Collections.Generic;

namespace Character.NPC {
    public class RiverEndNPC : NPC {
        [SerializeField] List<BaseItemData> possibleRewards;
        [SerializeField] CDF cdf;

        List<int> rewardIDs;

        private new void Start() {
            rewardIDs = new List<int>{0, 0, 0};
            cdf.CalculateCDF();
            base.Start();
        }

        public void UpdateChoices()
        {
            for (int i = 0; i < 3; i++)
            {
                rewardIDs[i] = cdf.GetRandomID();
                curStory.currentChoices[i].text = possibleRewards[rewardIDs[i]].itemName;
            }
        }
        public void RewardA()
        {
            UserStateManager.Instance.Backpack.AddItemToBackpack(possibleRewards[rewardIDs[0]].itemName, 1);
            Back();
        }
        public void RewardB()
        {
            UserStateManager.Instance.Backpack.AddItemToBackpack(possibleRewards[rewardIDs[1]].itemName, 1);
            Back();
        }
        public void RewardC()
        {
            UserStateManager.Instance.Backpack.AddItemToBackpack(possibleRewards[rewardIDs[2]].itemName, 1);
            Back();
        }

        public void Back()
        {
            Dialogue.DialogueManager.Instance.ExitDialogue();
            GameManager.Instance.UnloadScene();
        }


    }

}