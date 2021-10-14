using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour {
    [SerializeField] RectTransform head;
    [SerializeField] RectTransform mount;
    [SerializeField] float maxOffset;

    UnityEngine.UI.Image headImage;
    UnityEngine.UI.Image mountImage;
    Vector2 beginPos;

    void Start() {
        headImage = head.GetComponent<UnityEngine.UI.Image>();
        mountImage = mount.GetComponent<UnityEngine.UI.Image>();
        headImage.enabled = false;
        mountImage.enabled = false;
        InitHead();
    }

    void Update() {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
            beginPos = touch.position;
            transform.position = new Vector3(touch.position.x, touch.position.y, transform.position.z);
            ToggleImage(true);
        }
        else if (touch.phase == TouchPhase.Moved) {
            MoveHead(touch);
        }
        else if (touch.phase == TouchPhase.Ended) {
            ToggleImage(false);
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

    void ToggleImage(bool status) {
        headImage.enabled = status;
        mountImage.enabled = status;
    }

    public Vector2 GetOffset() {
        return head.anchoredPosition / maxOffset;
    }

}