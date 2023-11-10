using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Property {
    [SerializeField]
    private float _maxValue;
    private float _minValue;
    private float _currenValue;
    private float _multiplier = 1;
    public float MaxValue => _maxValue * _multiplier;
    public float MinValue => _minValue * _multiplier;
    public float CurrentValue {
        get => _currenValue * _multiplier;
        set {
            _currenValue = Mathf.Clamp(value, MinValue, MaxValue);
        }
    }
    
    public float Multiplier {
        get => _multiplier;
        set {
            _multiplier = value;
        }
    }
}

public class Damageable : MonoBehaviour
{
    // [SerializeField] float _maxhealth;
    [SerializeField] bool SoftDelete;
    [SerializeField] Property _health;
    public Property Health => _health;
    // private float _currentHealth;
    private bool _dead;
    public UnityEvent DiedEvent;
    // public float Health
    // {
    //     get => _currentHealth;
    //     set
    //     {
    //         _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
    //     }
    // }
    // public float MaxHealth => _maxhealth;

    private void Start()
    {
        _health.CurrentValue = _health.MaxValue;
        // _currentHealth = _maxhealth;
        // _startHealth = _maxhealth;
    }
    public void DealDamage(float amount)
    {
        if (amount <= 0)
        {
            return;
        }
        _health.CurrentValue -= amount;
        // _currentHealth -= amount;
        if (_health.CurrentValue <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (_dead)
        {
            return;
        }
        DiedEvent?.Invoke();
        if (SoftDelete)
        {
            foreach (var reneder in GetComponentsInChildren<Renderer>())
            {
                reneder.enabled = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        _dead = true;
    }
    
    public void AddToMulitiplier(float m) {
        _health.Multiplier += m;
        // _multiplier += m;
        // _currentHealth += _currentHealth * m;
        // _health.
        // _maxhealth = _startHealth * _multiplier;
    }
}
