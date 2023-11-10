using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeExecutor : RangeTargetExecutor
{
    public UnityEvent<Damageable> Event;
    [SerializeField] float _frequency;
    public bool IsActive {get => enabled && _target;}
    

    private void Start() {
        TargetSetEvent.AddListener(OnTargetSet);
        TargetOutOfRangeEvent.AddListener(OnTargetOut);
    }
    
    protected virtual void Execute() {}

    private void OnTargetSet(GameObject obj) {
        StopAllCoroutines();
        Debug.Assert(obj.GetComponent<Damageable>());
        StartCoroutine(ExecutionCourutine(obj.GetComponent<Damageable>()));
    }

    private void OnTargetOut(GameObject obj) {
        StopAllCoroutines();
    }
    
    private IEnumerator ExecutionCourutine(Damageable damageable) {
        while (IsActive) {
            yield return new WaitForSeconds( 1.0f / _frequency);
            if (damageable) {
                Event?.Invoke(damageable);
                Execute();
            } else {
                SetNewTarget();
            }
        }
    }
  
}
