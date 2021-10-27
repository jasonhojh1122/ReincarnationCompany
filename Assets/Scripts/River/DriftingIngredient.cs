
using UnityEngine;

namespace River {
    public class DriftingIngredient : DriftingItem {

        public override void GestureSatisfied() {
            boat.AddToMoney(data.pikeUpPrice);
        }

        public override void GestureFailed() {
            Destroy(gameObject);
        }

    }

}