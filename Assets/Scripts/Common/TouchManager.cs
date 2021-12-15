using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour {
    Dictionary<int, ITouchable> activeTouchable;
    private void Awake() {
        activeTouchable = new Dictionary<int, ITouchable>();
    }
    private void Update() {

        for (int i = 0; i < Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);
            if (activeTouchable.ContainsKey(touch.fingerId)) {
                activeTouchable[touch.fingerId].Touched(touch);
                if (touch.phase == TouchPhase.Ended) {
                    activeTouchable.Remove(touch.fingerId);
                }
            }
            else if (ProcessTouchOnUI(touch)) {
                // Debug.Log("UI");
                continue;
            }
            else if (ProcessTouchOnGameObject(touch)) {
                // Debug.Log("GameObject");
                continue;
            }
            else {
                // Debug.Log("Gesture" + Gesture.GestureManager.Instance.queue.Count);
                Gesture.GestureManager.Instance.UpdateTouch(touch);
            }
        }
    }

    bool ProcessTouchOnUI(Touch touch) {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, hits);
        foreach (RaycastResult hit in hits) {
            ITouchable touchable = hit.gameObject.GetComponent<ITouchable>();
            if (touchable != null && touchable.Touched(touch)) {
                activeTouchable.Add(touch.fingerId, touchable);
                return true;
            }
        }
        return false;
    }

    bool ProcessTouchOnGameObject(Touch touch) {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
        foreach (RaycastHit2D hit in hits) {
            if (hit.collider != null) {
                ITouchable touchable = hit.collider.gameObject.GetComponent<ITouchable>();
                if (touchable != null && touchable.Touched(touch)) {
                    activeTouchable.Add(touch.fingerId, touchable);
                    return true;
                }
            }
        }
        return false;
    }


}