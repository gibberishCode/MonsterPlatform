using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaftformPiece : MonoBehaviour
{
    [SerializeField] Vector3 _offset;
    // Start is called before the first frame update
    internal void Build(Tower towerPrefab)
    {
        var tower = Instantiate(towerPrefab, transform);
        tower.transform.localPosition = _offset;
        tower.transform.localRotation = Quaternion.identity;
    }
}
