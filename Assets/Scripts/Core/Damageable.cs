using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] float Health;
    private float _currentHealth;

    public UnityEvent DiedEvent;

    private void Start()
    {
        _currentHealth = Health;
    }
    public void DealDamage(float amount)
    {
        if (amount <= 0)
        {
            return;
        }
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        DiedEvent?.Invoke();
        Destroy(gameObject);
    }
}
