using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaftformPiece : MonoBehaviour
{
    [SerializeField] Vector3 _offset;
    [SerializeField] Animator _animator;
    private Damageable _damageable;
    public event Action OnDiedEvent;
    public bool IsMoving {get; set;}
    
    private void Start() {
        _damageable = GetComponent<Damageable>();
        _damageable.DiedEvent.AddListener(() => OnDiedEvent?.Invoke());
    }
    
    private void Update() {
        _animator.enabled = IsMoving;
    }

    internal void Build(Tower towerPrefab)
    {
        var tower = Instantiate(towerPrefab, transform);
        tower.transform.localPosition = _offset;
        tower.transform.localRotation = Quaternion.identity;
    }
}
