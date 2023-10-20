using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class AttackerData
{
    public float Damage;
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
        Debug.Assert(obj.GetComponent<Damageable>());
        StartCoroutine(AttackCourutine(obj.GetComponent<Damageable>()));
    }

    private void OnTargetOut(GameObject obj) {
        StopAllCoroutines();
    }

    private IEnumerator AttackCourutine(Damageable damageable) {
        while (IsAttacking) {
            yield return new WaitForSeconds( 1.0f / _data.Frequency);
            if (damageable) {
                AttackEvent?.Invoke(damageable.gameObject);
                damageable.DealDamage(_data.Damage);
            } else {
                SetNewTarget();
            }
        }
    }
  
}
