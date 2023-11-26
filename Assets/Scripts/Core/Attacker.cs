using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class AttackerData
{
    public Property Damage;
    public float Frequency;
}
public class Attacker : RangeTargetExecutor
{
    [SerializeField] AttackerData _data;
    public UnityEvent<GameObject> AttackEvent;
    // public bool IsAttacking => _target;
    public bool IsAttacking {get => enabled && _target;}
    

    private void Start() {
        TargetSetEvent.AddListener(OnTargetSet);
        TargetOutOfRangeEvent.AddListener(OnTargetOut);
    }

    private void OnTargetSet(GameObject obj) {
        StopAllCoroutines();
        var damageable = obj.GetComponentInParent<Damageable>();
        Debug.Assert(damageable);
        StartCoroutine(AttackCourutine(damageable));
        Debug.Log($"Set target {obj} for {gameObject}", transform);
    }

    private void OnTargetOut(GameObject obj) {
        StopAllCoroutines();
    }

    private IEnumerator AttackCourutine(Damageable damageable) {
        while (IsAttacking) {
            yield return new WaitForSeconds( 1.0f / _data.Frequency);
            if (damageable) {
                AttackEvent?.Invoke(damageable.gameObject);
                damageable.DealDamage(_data.Damage.MaxValue);
            } else {
                SetNewTarget();
            }
        }
    }
    public void AddToMulitiplier(float m) {
        _data.Damage.Multiplier += m;
    }
  
}
