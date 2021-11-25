
using UnityEngine;

namespace Character.NPC {
    public class RiverNPC : NPC {
        [SerializeField] int price = 100;
        public void CheckMoney() {
            Debug.Log("CheckMoney");
            if (UserStateManager.Instance.Money < price) {
                curStory.ChoosePathString("RunOutOfMoney");
            }
            else {
                curStory.ChoosePathString("EnoughMoney");
            }
        }

        public void Enter() {
            GameManager.Instance.LoadScene("01-River");
        }


    }

}