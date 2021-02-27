using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform leftLimit, rightLimit;
    public Transform target;
    private Vector2 velocity = Vector3.zero;

    public float smoothSpeed = 0.125f;
    public float offset;

    void FixedUpdate()
    {
        Vector2 desiredPosition = new Vector2(target.position.x + offset, 0);
        Vector2 smoothedPosition = Vector2.SmoothDamp(transform.position, desiredPosition,ref velocity,0.1f);
        if (smoothedPosition.x > leftLimit.position.x && smoothedPosition.x < rightLimit.position.x)
        {
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);
        }
    }
}
