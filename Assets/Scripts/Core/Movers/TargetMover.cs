using UnityEngine;
using Core;
using UnityEngine.Events;
using MyUnityHelpers;
using Vertx.Attributes;

public class TargetMover : Mover
{
    public UnityEvent ReachedTarget;
    // [SerializeReference, SubclassSelector]
    private ITarget _target;
    private bool _targetReported;
    public ITarget Target
    {
        get => _target;
        set {
            _target = value;
            _targetReported = false;
        }
    }

    private void Update()
    {
        UpdateDirection();
        _desiredVelocity = Direction == Vector3.zero ? Vector3.zero : Direction * DesiredSpeed;
        Move();

    }

    private void UpdateDirection()
    {
        if (_target == null)
        {
            return;
        }

        var vector = _target.Position.ZeroY() - transform.position.ZeroY();
        if (vector.sqrMagnitude < _settings.DistanceTollerance * _settings.DistanceTollerance)
        {
            // if (Direction != Vector3.zero)
            // {
            if (!_targetReported)
            {
                ReachedTarget?.Invoke();
                _targetReported = true;
            }
            // }
            Direction = Vector3.zero;
        }
        else
        {
            Direction = vector.normalized;
            _targetReported = false;
            // _desiredVelocity = Direction * _settings.MaxSpeed;
        }

    }

}