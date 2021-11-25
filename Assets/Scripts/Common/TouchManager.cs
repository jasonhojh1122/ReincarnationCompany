using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour {

    private void Update() {

        for (int i = 0; i < Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);
            if (ProcessTouchOnUI(touch)) {
                Debug.Log("UI");
                continue;
            }
            else if (ProcessTouchOnGameObject(touch)) {
                // Debug.Log("GameObject");
                continue;
            }
            else {
                // Debug.Log("Gesture");
                Gesture.GestureManager.Instance.UpdateTouch(touch);
            }
        }
    }

    bool ProcessTouchOnUI(Touch touch) {

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, hits);
        if (hits.Count > 0) {
            ITouchable touchable = hits[0].gameObject.GetComponent<ITouchable>();
            if (touchable != null) {
                touchable.Touched(touch);
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    bool ProcessTouchOnGameObject(Touch touch) {
        // Ray2D raycast = Camera.main.ScreenPointToRay2D(touch.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
        foreach (RaycastHit2D hit in hits) {
            if (hit.collider != null) {
                ITouchable touchable = hit.collider.gameObject.GetComponent<ITouchable>();
                if (touchable != null) {
                    touchable.Touched(touch);
                    return true;
                }
            }
        }
        return false;

        /* RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
        if (hit.collider != null) {
            ITouchable touchable = hit.collider.gameObject.GetComponent<ITouchable>();
            if (touchable == null) {
                return false;
            }
            else {
                touchable.Touched(touch);
                return true;
            }
        }
        else {
            return false;
        } */
    }


}