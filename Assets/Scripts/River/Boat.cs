using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace River {
    public class Boat : MonoBehaviour {

        [SerializeField] int life;
        [SerializeField] List<GrabPosition> grabPositions;
        [SerializeField] BoatLife boatLife;

        private void Start() {
            foreach (GrabPosition gp in grabPositions) {
                gp.SetBoat(this);
            }
        }

        public void AddToLife(int amount) {
            boatLife.AddToLife(amount);
            if (!boatLife.IsAlive()) {
                GameManager.Instance.LoadSceneAndClose("01-River-Fail");
            }
        }

        public void AddToMoney(int amount) {
            UserStateManager.Instance.Money += amount;
        }

        public void AddToBackpack(string name) {
            UserStateManager.Instance.Backpack.AddItemToBackpack(name, 1);
        }

    }
}