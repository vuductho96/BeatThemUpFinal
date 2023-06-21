using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 cameraOffset;
    public Quaternion cameraRotation;
    public float smoothness = 0.1f;

    private void LateUpdate()
    {
        // Calculate the target camera position and rotation
        Vector3 targetPosition = playerTransform.position + cameraOffset;
        Quaternion targetRotation = playerTransform.rotation * cameraRotation;

        // Smoothly interpolate the camera position and rotation towards the target
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothness);
    }
}
