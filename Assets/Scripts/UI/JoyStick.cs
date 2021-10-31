using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, ITouchable {
    [SerializeField] RectTransform head;
    [SerializeField] float maxOffset;
    [SerializeField] float horSpeed, verSpeed;
    RectTransform mount;
    Vector2 beginPos;
    int fingerID;
    [SerializeField] Rigidbody2D target;
    // [SerializeField] Transform target;

    void Start() {
        mount = GetComponent<RectTransform>();
        fingerID = -1;
        InitHead();
    }

    private void Update() {
        if (fingerID >= 0)
            MoveHead();
    }

    protected void FixedUpdate() {
        if (target != null)
            UpdatePos();
    }

    protected void UpdatePos() {
        Vector2 offset = GetOffset();
        Vector2 vel = new Vector2();
        vel.x += horSpeed * offset.x;
        vel.y += verSpeed * offset.y;
        target.MovePosition(target.position + vel * Time.deltaTime);
    }

    public void Touched(Touch touch) {
        if (touch.fingerId == fingerID) {
            return;
        }
        else if (touch.phase == TouchPhase.Began){
            fingerID = touch.fingerId;
            beginPos = touch.position;
        }
    }

    void InitHead() {
        head.anchoredPosition = Vector2.zero;
    }

    void MoveHead() {
        Touch touch = Input.GetTouch(fingerID);
        if (touch.phase == TouchPhase.Began) {
            beginPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended) {
            InitHead();
            fingerID = -1;
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