using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoverSettings {
    public float Speed;
    public float DistanceTollerance = 0.5f;
}

public class Mover : MonoBehaviour {
    [SerializeField] private MoverSettings _settings;
    private ITarget _target;

    public ITarget Target {
        get => _target;
        set => _target = value;
    }

    private void Update() {
        Move();
    }

    private void Move() {
        if (_target == null) {
            return;
        }

        var vector = _target.Position - transform.position;
        if (vector.sqrMagnitude < _settings.DistanceTollerance * _settings.DistanceTollerance) {
            return;
        }

        var dir = vector.normalized;
        transform.position += dir * _settings.Speed * Time.deltaTime;
    }
}