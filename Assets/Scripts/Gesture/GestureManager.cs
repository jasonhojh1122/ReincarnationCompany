using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace Gesture {

public class GestureManager : MonoBehaviour {

    [SerializeField] Indicator indicator;
    Queue<AGesture> queue;
    bool monitoring;

    private void Start() {
        ToggleIndicator(false);
        monitoring = false;
        queue = new Queue<AGesture>();
    }

    private void Update() {
        if (!monitoring && queue.Count > 0) {
            monitoring = true;
            StartCoroutine(MonitorGesture());
        }
    }

    public void UpdateTouch(Touch touch) {
        if (monitoring) {
            queue.Peek().UpdateTouch(touch);
        }
    }

    IEnumerator MonitorGesture() {
        AGesture gesture = queue.Peek();
        ToggleIndicator(true);
        gesture.StartGesture(indicator);

        while (!gesture.IsFailed() && !gesture.IsSatisfied() ) {
            yield return null;
        }
        if (gesture.IsFailed()) {
            gesture.OnFailed();
        }
        else if (gesture.IsSatisfied()) {
            gesture.OnSatisfied();
        }
        queue.Dequeue();
        ToggleIndicator(false);
        monitoring = false;
    }

    public void Enqueue(AGesture gesture) {
        queue.Enqueue(gesture);
    }

    public void ToggleIndicator(bool state) {
        indicator.gameObject.SetActive(state);
    }

    public void ClearQueue() {
        StopAllCoroutines();
        queue.Clear();
        monitoring = false;
    }

}

}
