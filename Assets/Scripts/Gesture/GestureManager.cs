using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace Gesture {

public class GestureManager : MonoBehaviour {

    private static GestureManager _instance;
    public static GestureManager Instance {
        get => _instance;
    }

    GesturePool gesturePool;
    public Queue<AGesture> queue;
    bool monitoring;

    public GesturePool GesturePool {
        get => gesturePool;
    }

    private void Awake() {
        _instance = this;
        gesturePool = new GesturePool();
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
        gesture.StartGesture();

        while (!gesture.IsFailed() && !gesture.IsSatisfied() ) {
            yield return null;
        }
        if (gesture.IsFailed()) {
            gesture.OnFailed.Invoke();
        }
        else if (gesture.IsSatisfied()) {
            gesture.OnSatisfied.Invoke();
        }
        if (queue.Count > 0)
            queue.Dequeue();
        monitoring = false;
    }

    public void Enqueue(AGesture gesture) {
        queue.Enqueue(gesture);
    }

    public void ClearQueue() {
        StopAllCoroutines();
        if (queue.Count > 0)
            queue.Clear();
        monitoring = false;
    }

}

}
