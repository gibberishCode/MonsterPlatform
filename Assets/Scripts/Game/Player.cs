using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using System.Linq;

public interface IMover
{
    float Speed { get; }
}

public class Player : MonoBehaviour, ITarget
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _slowDownFactor = 0.1f;

    private Mover _mover;
    private Attacker _attacker;
    [SerializeField]
    private PlayerAnimator _playerAnimator;
    private ResourceCollcetor _resourceCollector;
    // private Vector3 _velocity;

    // public float Speed => _velocity.magnitude;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _resourceCollector = GetComponent<ResourceCollcetor>();
        _resourceCollector.ResourceCollectedEvent.AddListener(
            (spot) => _playerAnimator.Shoot(spot.transform.position)
        );
        _resourceCollector.InRangeEvent.AddListener(OnInRange);
        _attacker.TargetSetEvent.AddListener(OnInRange);
    }

    private void OnInRange(GameObject obj)
    {
        var dir = transform.position - obj.transform.position;
        // var dir = obj.transform.position - transform.position;
        SetDirection(dir);
    }

    private void Update()
    {
        // _velocity += _direction * _acceleration * Time.deltaTime;
        // _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);
        // if (_direction.magnitude == 0)
        // {
        //     _velocity *= _slowDownFactor;
        // }
        // transform.position += _velocity * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        _mover.Direction = direction;
    }

    public Vector3 Position => transform.position;

}
