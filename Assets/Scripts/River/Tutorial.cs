using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections.Generic;

namespace River
{
    [System.Serializable]
    public struct TutorialClip {
        [TextArea(4,12)] public string text;
        public RectTransform focus;
        public PlayableDirector playableDirector;
    }

    public class Tutorial : MonoBehaviour {
        [SerializeField] TextMeshProUGUI tutorialText;
        [SerializeField] RectTransform focusMask;
        [SerializeField] Vector2 focusMaskPadding;
        [SerializeField] List<TutorialClip> clips;

        RectTransform canvasRect;
        Vector2 uiOffset;
        Vector2 focusMaskGlobal = new Vector2(1500.0f, 1500.0f);
        int id;

        private void Awake() {
            canvasRect = GetComponent<RectTransform>();
            uiOffset = new Vector2(canvasRect.sizeDelta.x * 0.5f, canvasRect.sizeDelta.y * 0.5f);
        }

        private void Start() {
            id = 0;
            StartNewClip();
        }

        void StartNewClip() {
            if (id < clips.Count) {
                tutorialText.text = clips[id].text;
                if (clips[id].focus != null) {
                    SetupFocusMask(clips[id].focus);
                }
                else {
                    focusMask.anchoredPosition = Vector2.zero;
                    focusMask.sizeDelta = focusMaskGlobal;
                }
                if (clips[id].playableDirector != null) {
                    clips[id].playableDirector.Play();
                }
                Gesture.GestureManager.Instance.Enqueue(NewTap());
                id += 1;
                Debug.Log("Start new clip");
            }
            else {
                GameManager.Instance.LoadSceneAndClose("01-River");
            }
        }

        void SetupFocusMask(RectTransform focus) {
            var wp = GetWorldSpaceCenterPoint(focus);
            Vector2 viewPortPos = Camera.main.WorldToViewportPoint(wp);
            Vector2 screenPos = new Vector2(viewPortPos.x * canvasRect.sizeDelta.x,
                                            viewPortPos.y * canvasRect.sizeDelta.y);
            screenPos -= uiOffset;
            focusMask.anchoredPosition = screenPos;
            focusMask.sizeDelta = focus.sizeDelta + focusMaskPadding;
        }

        Vector3 GetWorldSpaceCenterPoint(RectTransform rt)
        {
            Vector3[] v = new Vector3[4];
            Vector3 ret = Vector3.zero;
            rt.GetWorldCorners(v);
            for (var i = 0; i < 4; i++)
            {
                ret += v[i];
            }
            return ret/4.0f;
        }

        Gesture.Tap NewTap() {
            Gesture.Tap tap = new Gesture.Tap();
            tap.TargetCount = 1;
            tap.SingleDuration = float.MaxValue;
            tap.OnSingleSatisfied.AddListener(StartNewClip);
            return tap;
        }

        public void Skip() {
            GameManager.Instance.LoadSceneAndClose("01-River");
        }
    }


}

