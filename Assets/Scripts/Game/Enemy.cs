using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private TargetMover _mover;
    private Attacker _attacker;
    public float Speed => _mover.Speed;
    public float IsAttcking => _attacker.IsAttacking ? 1 : 0;

    private void Awake()
    {
        _mover = GetComponent<TargetMover>();
        _attacker = GetComponent<Attacker>();
        //TODO Fix it 
        _mover.Target = FindAnyObjectByType<Player>();
    }

}
