using UnityEngine;

public class CameraFolower : MonoBehaviour
{
    [SerializeField] Transform _target;           
    [SerializeField] float _smoothSpeed = 5.0f;    
    [SerializeField] Vector3 _offset = new Vector3(0, 2, -5);  
    [SerializeField] private bool _autoOffest;

    private void Start() {
        if (_autoOffest) {
            _offset = transform.position - _target.position;
        }
    }

    private void Update()
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
        // transform.LookAt(_target);   
    }
}
