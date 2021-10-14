using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMount : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag.name == "Head")
        {
            eventData.pointerDrag.transform.position = gameObject.transform.position;
        }
    }

}