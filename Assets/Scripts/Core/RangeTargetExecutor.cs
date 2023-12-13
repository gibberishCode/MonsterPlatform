using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeTargetExecutor : MonoBehaviour {
    public UnityEvent<GameObject> TargetSetEvent;
    public UnityEvent<GameObject> TargetOutOfRangeEvent;
    protected GameObject _target;
    public GameObject Target {
        get => _target;
        set {
            TargetOutOfRangeEvent?.Invoke(_target);
            _target = value;
            if (value) {
                TargetSetEvent.Invoke(value);
            }
        }
    }
    private List<GameObject> _targets = new List<GameObject>();

    private void OnEnable() {
        if (!Target) {
            SetNewTarget();
        }
    }

    public void InRange(GameObject obj) {
        if (_target == null) {
            Target = obj;
        } else {
            if (!_targets.Contains(obj)) {
                _targets.Add(obj);
            }
        }
    }
    
    private void Update() {
        if (_target == null) {
            SetNewTarget();
        }
    }

    public void OutRange(GameObject obj) {
        if (_target == obj) {
            SetNewTarget();
        }
        _targets.Remove(obj);
    }

    public void SetNewTarget() {
        foreach (var target in _targets) {
            if (target) {
                Target = target;
                _targets.Remove(target);
                return;
            }
        }
        Target = null;
    }
}