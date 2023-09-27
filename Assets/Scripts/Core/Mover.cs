using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
[System.Serializable]
public class MoverSettings
{
    public float Speed;
    public float DistanceTollerance = 0.5f;
}

public class Mover : MonoBehaviour
{
    [SerializeField] private MoverSettings _settings;
    private ITarget _target;
    private Vector3 _velocity;

    public float Speed => _velocity.magnitude / _settings.Speed;
    public ITarget Target
    {
        get => _target;
        set => _target = value;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_target == null)
        {
            return;
        }

        var vector = _target.Position - transform.position;
        if (vector.sqrMagnitude < _settings.DistanceTollerance * _settings.DistanceTollerance)
        {
            _velocity = Vector3.zero;
            return;
        }

        var dir = vector.normalized;
        _velocity = dir * _settings.Speed;
        transform.position += _velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(dir);
    }
}