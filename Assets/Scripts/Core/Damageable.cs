using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] bool SoftDelete;
    private float _currentHealth;
    private bool _dead;
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
        if (_dead) {
            return;
        }
        DiedEvent?.Invoke();
        if (SoftDelete) {
            foreach (var reneder in GetComponentsInChildren<Renderer>()) {
                reneder.enabled = false;
            }
        } else  {
            Destroy(gameObject);
        }
        _dead = true;
    }
}
