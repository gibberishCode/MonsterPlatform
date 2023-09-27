using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUpdater : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Enemy _enemey;

    private void Update()
    {
        _animator.SetFloat("Speed", _enemey.Speed);
        _animator.SetFloat("IsAttacking", _enemey.IsAttcking);
    }

}
