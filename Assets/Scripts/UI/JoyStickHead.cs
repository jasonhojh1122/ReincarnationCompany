
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickHead : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private float maxDist;

    private RectTransform rect;

    void Start() {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
    }


    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        rect.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        if (Vector3.Distance(Vector2.zero, rect.anchoredPosition) > maxDist) {
            Vector3 dir = rect.anchoredPosition;
            Debug.Log("Maxxxxx");
            rect.anchoredPosition = dir.normalized * maxDist;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
    }
}