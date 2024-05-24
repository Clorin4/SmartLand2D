using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.123f;
    public Vector3 offset = new Vector3(0.2f, 0.0f, -10f);
    public float dampingTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void MoveCamera(bool smooth)
    {
        Vector3 targetPosition = target.position + offset;

        if (smooth)
        {
            this.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, dampingTime);
        }
        else
        {
            this.transform.position = targetPosition;
        }
    }

    private void LateUpdate()
    {
        MoveCamera(true);
    }
}
