using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;
using Vertx.Attributes;

public class AnimatorUpdater : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Attacker _attacker;
    [SerializeField] Mover _mover;


    private void Start()
    {
        Debug.Assert(_attacker, this);
        Debug.Assert(_mover, this);
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _mover.Speed);
        _animator.SetFloat("IsAttacking", _attacker.IsAttacking ? 1 : 0);
    }

}
