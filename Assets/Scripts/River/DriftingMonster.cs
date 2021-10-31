
using UnityEngine;

namespace River {
    public class DriftingMonster : DriftingItem {

        public override void GestureSatisfied() {
            boat.AddToMoney(data.pikeUpPrice);
            Destroy(gameObject);
        }

        public override void GestureFailed() {
            boat.AddToLife(-1);
            Destroy(gameObject);
        }

    }

}