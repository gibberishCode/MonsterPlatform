using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using System.Linq;
using MyUnityHelpers;

public interface IMover
{
    float Speed { get; }
}

public enum PlayerState
{
    Idle,
    Moving,
    Attacking,
    IdlePlatfom,
    CollectingResources,
    StackInTowerState

}

public class PlayerData { 
    public float HealthMultiplier = 1;
    public float AttackMultiplier = 1;

}

public class Player : MonoBehaviour, ITarget, IUpgradeTarget
{

    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _slowDownFactor = 0.1f;
    [SerializeField] private Platform _platform;
    [SerializeField] private PlayerAnimator _playerAnimator;

    public PlayerData PlayerData { get; set; }
    public Vector3 Position => transform.position;

    private Mover _mover;
    private Rigidbody _rb;
    private Attacker _attacker;
  
    private ResourceCollcetor _resourceCollector;
    private PlayerState _state;
    private bool _isOnPlatfom;
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
        _attacker.TargetSetEvent.AddListener(OnInRangeEnemy);
        // _resourceCollector.InRangeEvent.AddListener(OnInRangeResource);
        _rb = GetComponent<Rigidbody>();
    }

    private void OnGUI()
    {
        GUIStyle headStyle = new GUIStyle();
        headStyle.fontSize = 50;
        headStyle.fontStyle = FontStyle.Bold;
        var rect = new Rect(250, 100, 250, 300);
        GUI.Label(rect, _state.ToString(), headStyle);
    }

    public void OnPlatformState(bool state)
    {
        _isOnPlatfom = state;
        if (state)
        {
            _rb.interpolation = RigidbodyInterpolation.None;
        }
        else
        {
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }

    private void SetState(PlayerState state)
    {
        _state = state;
        switch (_state)
        {
            case PlayerState.Idle:
                _resourceCollector.enabled = true;
                _attacker.enabled = true;
                // _rb.interpolation = RigidbodyInterpolation.None;
                break;
            case PlayerState.Moving:
                _resourceCollector.enabled = false;
                // _attacker.Stop();
                _attacker.enabled = false;

                // _rb.interpolation = RigidbodyInterpolation.Interpolate;
                break;
            case PlayerState.Attacking:
                _resourceCollector.enabled = false;
                _attacker.enabled = true;
                // _attacker.StartAttack();
                break;
            case PlayerState.IdlePlatfom:
                _resourceCollector.enabled = false;
                _attacker.enabled = true;
                // _rb.interpolation = RigidbodyInterpolation.None;
                break;
            case PlayerState.CollectingResources:
                _resourceCollector.enabled = true;
                _attacker.enabled = true;
                break;
        }
    }

    private void OnInRangeResource(GameObject resource)
    {
        SetState(PlayerState.CollectingResources);
        FaceTarget(resource);
    }

    private void OnInRangeEnemy(GameObject enemy)
    {
        SetState(PlayerState.Attacking);
        FaceTarget(enemy);
    }

    public void OnAttack(GameObject target)
    {
        FaceTarget(target);
    }

    private void FaceTarget(GameObject target)
    {
        var dir = target.transform.position - transform.position;
        SetDirection(dir);
    }

    private void Update()
    {
        switch (_state)
        {
            case PlayerState.Idle:
                if (_mover.Speed >= 0.01f)
                {
                    SetState(PlayerState.Moving);
                }
                break;
            case PlayerState.Moving:
                if (_mover.Speed <= 0.01f)
                {

                    if (_isOnPlatfom)
                    {
                        SetState(PlayerState.IdlePlatfom);
                    }
                    else
                    {
                        SetState(PlayerState.Idle);
                    }
                }
                break;
            case PlayerState.Attacking:
                if (_mover.Speed >= 0.01f)
                {
                    SetState(PlayerState.Moving);
                }
                break;
            case PlayerState.IdlePlatfom:
                if (_mover.Speed >= 0.01f)
                {
                    SetState(PlayerState.Moving);
                }
                break;
            case PlayerState.CollectingResources:
                if (_mover.Speed >= 0.01f)
                {
                    SetState(PlayerState.Moving);
                }
                break;
            case PlayerState.StackInTowerState:
                var dir = _platform.transform.position.ZeroY() - transform.position.ZeroY();
                _rb.AddForce(dir.normalized * 100);
                break;
        }
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
        if (direction == Vector3.zero)
        {
            _mover.DesiredSpeed = 0;
        }
        else
        {
            _mover.DesiredSpeed = _mover.MaxSpeed;
            // _mover.DesiredSpeed = _mover.MaxSpeed;
            _mover.Direction = direction;
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Tower>()) {
            SetState(PlayerState.StackInTowerState);
        }   
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<Tower>()) {
            SetState(PlayerState.Idle);
        }   
    }

}
