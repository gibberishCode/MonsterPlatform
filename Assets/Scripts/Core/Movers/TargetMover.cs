using UnityEngine;
using Core;
using UnityEngine.Events;

public class TargetMover : Mover
{
    public UnityEvent ReachedTarget;
    private ITarget _target;
    public ITarget Target
    {
        get => _target;
        set => _target = value;
    }

    private void Update()
    {
        UpdateDirection();
        _desiredVelocity = Direction == Vector3.zero ? Vector3.zero : Direction * _settings.MaxSpeed;
        Move();

    }

    private void UpdateDirection()
    {
        if (_target == null)
        {
            return;
        }

        var vector = _target.Position - transform.position;
        if (vector.sqrMagnitude < _settings.DistanceTollerance * _settings.DistanceTollerance)
        {
            if (Direction != Vector3.zero)
            {
                ReachedTarget?.Invoke();
            }
            Direction = Vector3.zero;
        }
        else
        {
            Direction = vector.normalized;
            // _desiredVelocity = Direction * _settings.MaxSpeed;
        }

    }

}