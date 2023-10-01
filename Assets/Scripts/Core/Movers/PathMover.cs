using System.Collections.Generic;
using Core;
using UnityEngine;

public class WaypointTarget : ITarget
{
    public Vector3 Position { get; set; }
}

public class PathMover : TargetMover
{
    public List<Vector3> TestPath = new List<Vector3>();
    public bool Loop;
    private List<Vector3> _path;
    private int _currentWaypoint;

    private void Start()
    {
        _path = TestPath;
        if (_path.Count > 0)
        {
            Target = new WaypointTarget() { Position = _path[_currentWaypoint] };
        }
        ReachedTarget.AddListener(OnReachedTarget);

    }

    private void OnReachedTarget()
    {
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
            Target = new WaypointTarget() { Position = _path[_currentWaypoint] };
        }

    }


}
