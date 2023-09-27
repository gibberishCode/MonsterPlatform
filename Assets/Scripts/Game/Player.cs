using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class Player : MonoBehaviour, ITarget {
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _slowDownFactor = 0.1f;
    private Vector3 _direction;
    private Vector3 _velocity;
    
    private void Update() {
        _velocity += _direction * _acceleration * Time.deltaTime;
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);
        if (_direction.magnitude == 0) {
            _velocity *= _slowDownFactor;
        }
        transform.position += _velocity * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction) {
        _direction = direction;
    }

    public Vector3 Position => transform.position;
}
