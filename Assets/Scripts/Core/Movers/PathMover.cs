using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Vertx;
using Vertx.Attributes;

[System.Serializable]
public class WaypointTarget : ITarget
{
    [SerializeField]
    private Vector3 _position;
    public Vector3 Position { get => _position; set => _position = value; }
}


[System.Serializable]
public class TransformTarget : ITarget
{
    [SerializeField]
    private Transform _transform;
    public Vector3 Position { get => _transform.position; }
}

public class PathMover : TargetMover
{
    [SerializeReference, SubclassSelector]
    public List<ITarget> TestPath = new List<ITarget>();
    public float WaitTime = 20;
    public bool Loop;
    public event Action WaitingEvent;
    public event Action MovingEvent;
    private List<ITarget> _path = new List<ITarget>();
    private int _currentWaypoint;

    private void Awake() {
        
        ReachedTarget.AddListener(OnReachedTarget);
    }

    private void Start()
    {
        _path = TestPath;
        if (_path.Count > 0)
        {
            Target = new WaypointTarget() { Position = _path[_currentWaypoint].Position };
        }
        MovingEvent?.Invoke();

    }

    private void OnReachedTarget()
    {
        StartCoroutine(WaitAndNext());
    }

    private IEnumerator WaitAndNext()
    {
        WaitingEvent?.Invoke();
        yield return new WaitForSeconds(WaitTime);
        _currentWaypoint++;
        if (_currentWaypoint >= _path.Count)
        {
            if (Loop)
            {
                _currentWaypoint %= _path.Count;
            }
            else
            {
                // TODO Reached end
            }
        }
        else
        {
            Target = new WaypointTarget() { Position = _path[_currentWaypoint].Position };
        }
        MovingEvent?.Invoke();
    }


}
