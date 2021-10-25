using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    [SerializeField] JoyStick joyStick;
    [SerializeField] float widthSpeed, depthSpeed;

    protected virtual void FixedUpdate() {
        UpdatePos();
    }

    protected virtual void UpdatePos() {
        Vector2 offset = joyStick.GetOffset();

        Vector3 newPos = transform.position;
        newPos.x += depthSpeed * offset.x * Time.deltaTime;
        newPos.y += depthSpeed * offset.y * Time.deltaTime;

        transform.position = newPos;
    }

}
