using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAnimator : MonoBehaviour
{
    [SerializeField] Animator _animator;
    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        var animSpeed = _mover.Speed / _mover.MaxSpeed;
        _animator.SetFloat("Speed", animSpeed);
    }



}
