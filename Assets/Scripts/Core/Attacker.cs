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
public class Attacker : MonoBehaviour
{
    public UnityEvent<GameObject> TargetSetEvent;
    [SerializeField] AttackerData _data;
    private Damageable _currentTarget;
    private FrequencyExecutor _attackTimer;
    public bool IsAttacking => _currentTarget;

    private void Start()
    {

    }

    public void Init(AttackerData data)
    {
        _data = data;
    }


    private void OnInRange(GameObject gameObject)
    {
        var target = gameObject.GetComponent<Damageable>();
        if (target)
        {
            _currentTarget = target;
            _attackTimer = new FrequencyExecutor(_data.Frequency, this, Attack);
            TargetSetEvent?.Invoke(gameObject);
        }
    }

    private void OnOutRange(GameObject gameObject)
    {
        if (_currentTarget && _currentTarget.gameObject == gameObject)
        {
            _currentTarget = null;
        }
    }

    private void Attack()
    {
        if (!_currentTarget)
        {
            _attackTimer = null;
            return;
        }
        _currentTarget.DealDamage(_data.Damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnInRange(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        OnOutRange(other.gameObject);
    }
}
