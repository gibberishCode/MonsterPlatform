using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolower : MonoBehaviour
{
    [SerializeField] Transform _target;             // The target to follow.
    [SerializeField] float _smoothSpeed = 5.0f;    // The speed at which the camera follows the target.
    [SerializeField] Vector3 _offset = new Vector3(0, 2, -5);  // The offset from the target.
    [SerializeField] private bool _autoOffest;

    private void Start() {
        if (_autoOffest) {
            _offset = transform.position - _target.position;
        }
    }

    private void LateUpdate()
    {
        if (_target == null)
        {
            Debug.LogWarning("Camera target not set.");
            return;
        }

        // Calculate the desired position of the camera.
        Vector3 desiredPosition = _target.position + _offset;

        // Smoothly interpolate between the current position and the desired position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Make the camera look at the target.
        transform.LookAt(_target);
    }
}
