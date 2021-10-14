using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    [SerializeField] JoyStick joyStick;
    [SerializeField] float minZ, maxZ, minXAtMinZ, maxXAtMinZ;
    [SerializeField] float widthSpeed, depthSpeed;

    protected virtual void FixedUpdate() {
        UpdatePos();
    }

    protected virtual void UpdatePos() {
        Vector2 offset = joyStick.GetOffset();

        Vector3 newPos = gameObject.transform.position;
        newPos.x += depthSpeed * offset.x * Time.deltaTime;
        newPos.z += depthSpeed * offset.y * Time.deltaTime;
        newPos.z = Mathf.Clamp(newPos.z, minZ, maxZ);
        float xOffset = Mathf.Tan(30 * Mathf.Deg2Rad) * Mathf.Abs(newPos.z - minZ);
        Debug.Log(xOffset);
        newPos.x = Mathf.Clamp(newPos.x, minXAtMinZ - xOffset, maxXAtMinZ + xOffset);
        gameObject.transform.position = newPos;
    }

}
