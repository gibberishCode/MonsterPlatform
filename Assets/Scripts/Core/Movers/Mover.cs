using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Unity.VisualScripting;
[System.Serializable]
public class MoverSettings
{
    public float Acceleration = 10;
    public float MaxSpeed;
    public float RotationSpeed;
    public float DistanceTollerance = 0.5f;
}

public class Mover : MonoBehaviour, IMover
{
    [SerializeField] protected MoverSettings _settings;
    [SerializeField] private bool _usePhysics;

    protected Vector3 _desiredVelocity;
    protected Vector3 _velocity;
    private Rigidbody _rb;

    public float Speed => _velocity.magnitude;
    public float MaxSpeed => _settings.MaxSpeed;
    public float DesiredSpeed { get; set; }
    public Vector3 Direction { get; set; }


    private void Awake()
    {
        if (_usePhysics)
        {
            _rb = GetComponent<Rigidbody>();
        }
    }

    private void Start()
    {
        DesiredSpeed = MaxSpeed;
    }

    private void Update()
    {
        _desiredVelocity = Direction * DesiredSpeed;
        if (!_usePhysics)
        {
            Move();
        }
    }

    private void FixedUpdate()
    {

        if (_usePhysics)
        {
            Move();
        }
    }

    // protected virtual void UpdateDesiredVelocity()
    // {
    //     _desiredVelocity = Direction * _settings.MaxSpeed;
    // }

    protected virtual void Move()
    {
        Vector3 acceleration = _desiredVelocity - _velocity;
        acceleration = Vector3.ClampMagnitude(acceleration, _settings.Acceleration * Time.deltaTime);
        Vector3 newVelocity = _velocity + acceleration;
        _velocity = Vector3.ClampMagnitude(newVelocity, _settings.MaxSpeed);

        if (_usePhysics)
        {
            if (_velocity.magnitude < 0.1f)
            {
                _velocity = Vector3.zero;
            }
            // _rb.AddForce(_velocity, ForceMode.VelocityChange);
            _rb.velocity = _velocity;
        }
        else
        {

            transform.position += _velocity * Time.deltaTime;
        }
        if (Direction != Vector3.zero)
        {

            var targetRot = Quaternion.LookRotation(Direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, _settings.RotationSpeed * Time.deltaTime);
        }
    }
}