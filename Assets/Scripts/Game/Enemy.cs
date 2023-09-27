using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Mover _mover;
    private Attacker _attacker;
    public float Speed => _mover.Speed;
    public float IsAttcking => _attacker.IsAttacking ? 1 : 0;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _mover.Target = _player;
    }

}
