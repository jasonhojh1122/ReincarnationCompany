using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour {

    [SerializeField] private Gesture.GestureManager gestureManager;

    private void Update() {

        for (int i = 0; i < Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);
            if (ProcessTouchOnUI(touch)) {
                continue;
            }
            else if (ProcessTouchOnGameObject(touch)) {
                continue;
            }
            else {
                gestureManager.UpdateTouch(touch);
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
        }
        return false;
    }

    bool ProcessTouchOnGameObject(Touch touch) {
        Ray raycast = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(raycast, out hit)) {
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
        }
    }


}