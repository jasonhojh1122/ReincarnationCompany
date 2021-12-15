using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, ITouchable {
    [SerializeField] float maxOffset;
    [SerializeField] RectTransform head;
    [SerializeField] Character.MovingTarget target;
    RectTransform mount;
    Vector2 beginPos;
    int fingerId;

    public Character.MovingTarget Target {
        get => target;
        set => target = value;
    }

    void Start() {
        fingerId = -1;
        mount = GetComponent<RectTransform>();
        InitHead();
    }

    public bool Touched(Touch touch) {
        if (touch.phase == TouchPhase.Began) {
            fingerId = touch.fingerId;
            beginPos = touch.position;
        }
        else if (touch.fingerId != fingerId) {
            return false;
        }

        MoveHead(touch);
        UpdateTargetPos();

        return true;
    }
    protected void UpdateTargetPos() {
        if (target == null) return;
        Vector2 offset = GetOffset();
        target.Move(GetOffset());
    }

    void InitHead() {
        head.anchoredPosition = Vector2.zero;
    }

    void MoveHead(Touch touch) {
        if (touch.phase == TouchPhase.Began) {
            beginPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended) {
            InitHead();
            fingerId = -1;
            return;
        }

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