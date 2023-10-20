using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformAttacher : MonoBehaviour
{
    public UnityEvent<bool> AttachedState;
    [SerializeField] LayerMask _platform;
    private Transform _platformTransform;

    // Update is called once per frame
    void Update()
    {


        if (Physics.Raycast(new Ray(transform.position + Vector3.up * 5, Vector3.down), out RaycastHit hit, 20, _platform))
        {
            if (transform.parent != hit.collider.transform)
            {
                _platformTransform = hit.collider.transform;
                transform.parent = _platformTransform;
                AttachedState?.Invoke(true);
            }
        }
        else
        {
            if (transform.parent == _platformTransform)
            {
                transform.parent = null;
                AttachedState?.Invoke(false);
            }
        }

    }
}
