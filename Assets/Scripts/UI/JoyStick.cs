using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, ITouchable {
    [SerializeField] RectTransform head;
    [SerializeField] float maxOffset;
    RectTransform mount;
    Vector2 beginPos;

    void Start() {
        mount = GetComponent<RectTransform>();
        InitHead();
    }

    public void Touched(Touch touch) {

        if (touch.phase == TouchPhase.Began) {
            beginPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved) {
            MoveHead(touch);
        }
        else if (touch.phase == TouchPhase.Ended) {
            InitHead();
        }

    }

    void InitHead() {
        head.anchoredPosition = Vector2.zero;
    }

    void MoveHead(Touch touch) {
        Vector2 offset = touch.position - beginPos;
        if (offset.magnitude > maxOffset) {
            offset = offset.normalized * maxOffset;
        }
        head.anchoredPosition = offset;
    }

    public Vector2 GetOffset() {
        return head.anchoredPosition / maxOffset;
    }

}