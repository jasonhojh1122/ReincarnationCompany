using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectButton : MonoBehaviour
{
    public UnityEvent mouseDownEvent;
    public UnityEvent mouseUpEvent;

    private void OnMouseDown()
    {
        if (mouseDownEvent != null)
        {
            mouseDownEvent.Invoke();
        }
    }
    private void OnMouseUp()
    {
        if (mouseUpEvent != null)
        {
            mouseUpEvent.Invoke();
        }
    }
}

