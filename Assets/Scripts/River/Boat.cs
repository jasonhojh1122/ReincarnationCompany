using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace River {
    public class Boat : MonoBehaviour {

        [SerializeField] int life;
        [SerializeField] List<GrabPosition> grabPositions;
        [SerializeField] BoatLife boatLife;
        [SerializeField] RiverGestureIndicator indicator;
        [SerializeField] TextMeshProUGUI moneyText;
        [SerializeField] Transform moneyGainPrefab;
        [SerializeField] Transform itemPickupPrefab;

        public RiverGestureIndicator Indicator {
            get => indicator;
        }

        private void Start() {
            foreach (GrabPosition gp in grabPositions) {
                gp.SetBoat(this);
            }
            indicator.gameObject.SetActive(false);
        }

        public void AddToLife(int amount) {
            boatLife.AddToLife(amount);
        }

        public void AddToMoney(int amount) {
            UserStateManager.Instance.Money += amount;
        }

        public void AddToBackpack(string name) {
            UserStateManager.Instance.Backpack.AddItemToBackpack(name, 1);
        }

        public void ShowIndicator(Gesture.AGesture gesture) {
            indicator.gameObject.SetActive(true);
            indicator.Init(gesture);
        }

        public void HideIndicator() {
            indicator.gameObject.SetActive(false);
        }

        public void ShowMoneyGain() {

        }

        public void ShowItemPickup() {

        }

    }
}