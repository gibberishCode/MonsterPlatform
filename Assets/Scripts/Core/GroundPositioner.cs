using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPositioner : MonoBehaviour
{
    [SerializeField] LayerMask _ground;

    private void Update()
    {
        if (Physics.Raycast(new Ray(transform.position + Vector3.up * 5, Vector3.down), out RaycastHit hit, 100, _ground))
        {
            var pos = transform.position;
            pos.y = hit.point.y + 1;
            transform.position = pos;
        }
    }
}
