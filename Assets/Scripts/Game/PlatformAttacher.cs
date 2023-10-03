using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttacher : MonoBehaviour
{
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
            }
        }
        else
        {
            if (transform.parent == _platformTransform)
            {
                transform.parent = null;
            }
        }

    }
}
