
using UnityEngine;

namespace River {
    public class DriftingIngredient : DriftingItem {

        public override void GestureSatisfied() {
            boat.AddToMoney(data.pikeUpPrice);
            boat.AddToBackpack(data.baseData.itemName);
            Debug.Log("Pickup" + data.baseData.itemName);
            Destroy(gameObject);
        }

        public override void GestureFailed() {
            Debug.Log("Ingredient failed");
            Destroy(gameObject);
        }

    }

}