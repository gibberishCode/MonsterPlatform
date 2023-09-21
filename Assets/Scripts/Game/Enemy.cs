using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private Player _player;
    private Mover _mover;

    private void Awake() {
        _mover = GetComponent<Mover>();
        _mover.Target = _player;
    }
    
}
