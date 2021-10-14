using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    [SerializeField] JoyStick joyStick;
    [SerializeField] Transform rightFront, rightBack, leftFront, leftBack;
    [SerializeField] float widthSpeed, depthSpeed;

    float xOffsetMax;
    float zLength;
    void Start() {
        xOffsetMax = ( (rightBack.position.x - leftBack.position.x) -
            (rightFront.position.x - leftFront.position.x) )/2;
        zLength = rightBack.position.z - rightFront.position.z;
    }
    protected virtual void FixedUpdate() {
        UpdatePos();
    }

    protected virtual void UpdatePos() {
        Vector2 offset = joyStick.GetOffset();

        Vector3 newPos = transform.position;
        newPos.x += depthSpeed * offset.x * Time.deltaTime;
        newPos.z += depthSpeed * offset.y * Time.deltaTime;
        newPos.z = Mathf.Clamp(newPos.z, rightFront.position.z, leftBack.position.z);

        float zDelta = transform.position.z - rightFront.position.z;
        float xOffset = zDelta * xOffsetMax / zLength;
        newPos.x = Mathf.Clamp(newPos.x, leftBack.position.x - xOffset, rightBack.position.x + xOffset);

        transform.position = newPos;
    }

}
