using UnityEngine;
using TMPro;

namespace River {
    [RequireComponent(typeof(CanvasGroupFader))]
    public class RiverGestureIndicator : MonoBehaviour {

        [SerializeField] RectTransform icon;
        [SerializeField] UnityEngine.UI.Image iconImage;
        [SerializeField] GameObject vertical;
        [SerializeField] GameObject horizontal;
        [SerializeField] TextMeshProUGUI countText;
        [SerializeField] Color32 iconTint;
        [SerializeField] float enlargeFactor;
        [SerializeField] float enlargeTranDur;
        [SerializeField] float enlargeDur;
        [SerializeField] float rectSize;
        [SerializeField] float speed;

        CanvasGroupFader fader;
        Gesture.AGesture gesture;
        int count;

        public int Count {
            get => count;
            set {
                count = value;
                countText.text = count.ToString();
            }
        }

        Vector3 enlargeScale;

        private void Awake() {
            fader = GetComponent<CanvasGroupFader>();
            enlargeScale = new Vector3(enlargeFactor, enlargeFactor, enlargeFactor);
            vertical.SetActive(false);
            horizontal.SetActive(false);
        }

        public void ResetCount() {
            Count = gesture.TargetCount;
            icon.transform.localScale = Vector3.one;
            icon.anchoredPosition = Vector3.zero;
        }

        public void UpdateCount() {
            Count = gesture.RemainCount;
            StartCoroutine(Enlarge());
        }

        void StartVertical() {
            vertical.SetActive(true);
            StartCoroutine(VerticalAnim());
        }

        void StartHorizontal() {
            horizontal.SetActive(true);
            StartCoroutine(HorizontalAnim());
        }

        System.Collections.IEnumerator VerticalAnim() {
            bool upDir = true;
            while (true) {
                if (upDir) {
                    Vector3 newPos = icon.anchoredPosition;
                    newPos.y += speed * Time.deltaTime;
                    if (newPos.y > rectSize) {
                        newPos.y = rectSize;
                        upDir = false;
                    }
                    icon.anchoredPosition = newPos;
                }
                else {
                    Vector3 newPos = icon.anchoredPosition;
                    newPos.y -= speed * Time.deltaTime;
                    if (newPos.y < -rectSize) {
                        newPos.y = -rectSize;
                        upDir = true;
                    }
                    icon.anchoredPosition = newPos;
                }
                yield return null;
            }
        }

        System.Collections.IEnumerator HorizontalAnim() {
            bool rightDir = true;
            while (true) {
                if (rightDir) {
                    Vector3 newPos = icon.anchoredPosition;
                    newPos.x += speed * Time.deltaTime;
                    if (newPos.x > rectSize) {
                        newPos.x = rectSize;
                        rightDir = false;
                    }
                    icon.anchoredPosition = newPos;
                }
                else {
                    Vector3 newPos = icon.anchoredPosition;
                    newPos.x -= speed * Time.deltaTime;
                    if (newPos.x < -rectSize) {
                        newPos.x = -rectSize;
                        rightDir = true;
                    }
                    icon.anchoredPosition = newPos;
                }
                yield return null;
            }
        }

        System.Collections.IEnumerator Enlarge() {
            float t = 0.0f;
            while (t < enlargeTranDur)
            {
                float p = t / enlargeTranDur;
                icon.transform.localScale = Vector3.Lerp(Vector3.one, enlargeScale, p);
                iconImage.color = Color32.Lerp(Color.white, iconTint, p);
                t += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(enlargeDur);
            t = 0.0f;
            while (t < enlargeTranDur)
            {
                float p = t / enlargeTranDur;
                icon.transform.localScale = Vector3.Lerp(enlargeScale, Vector3.one, p);
                iconImage.color = Color32.Lerp(iconTint, Color.white, p);
                t += Time.deltaTime;
                yield return null;
            }
        }

        public void Show(Gesture.AGesture gesture) {
            fader.FadeIn();
            icon.anchoredPosition = Vector3.zero;
            this.gesture = gesture;
            Count = this.gesture.RemainCount;
            StopAllCoroutines();
            switch(gesture.GetName())
            {
                case "Tap":
                    break;
                case "HorizontalMove":
                    StartHorizontal();
                    break;
                case "VerticalMove":
                    StartVertical();
                    break;
            }
        }

        public void End() {
            vertical.SetActive(false);
            horizontal.SetActive(false);
            icon.anchoredPosition = Vector3.zero;
            fader.FadeOut();
        }

    }

}