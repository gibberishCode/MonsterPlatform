using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ColliderDrawer : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        var boxes = GetComponentsInChildren<BoxCollider>();
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        foreach (var box in boxes)
        {
            Gizmos.DrawCube(transform.position + box.center, box.size);
        }
    }
}
