using UnityEngine;
using TMPro;

namespace River {
    public class RiverGestureIndicator : MonoBehaviour {

        [SerializeField] SpriteRenderer sr;
        [SerializeField] TextMeshPro gestureName;
        [SerializeField] TextMeshPro countText;
        [SerializeField] float enlargeFactor;
        [SerializeField] float enlargeTime;

        Gesture.AGesture gesture;
        int count;

        public int Count {
            get => count;
            set {
                count = value;
                countText.text = "X" + count.ToString();
            }
        }

        Vector3 enlargeScale;

        private void Start() {
            enlargeScale = new Vector3(enlargeFactor, enlargeFactor, enlargeFactor);
        }

        public void Init(Gesture.AGesture gesture) {
            this.gesture = gesture;
            Reset();
        }

        public void Reset() {
            gestureName.text = this.gesture.GetName();
            Count = gesture.TargetCount;
            sr.sprite = gesture.GestureData.indicateSprite;
            transform.localScale = Vector3.one;
        }

        public void UpdateCount() {
            Count = gesture.RemainCount;
            StartCoroutine(Enlarge());
        }

        System.Collections.IEnumerator Enlarge() {
            transform.localScale = enlargeScale;
            yield return new WaitForSeconds(enlargeTime);
            transform.localScale = Vector3.one;
        }

    }

}