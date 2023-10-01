using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Attacker _attacker;
    [SerializeField] Mover _mover;

    public bool ikActive = false;
    public float To = 0.5f;
    public float Back = 0.7f;
    private float _ik = 0;
    private Vector3 _shootTarget;

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

    public void Shoot(Vector3 target)
    {

        StartCoroutine(_Shoot(target));
    }

    private IEnumerator _Shoot(Vector3 target)
    {
        _shootTarget = target;
        float t = 0;
        while (t < To)
        {
            t += Time.deltaTime;
            _ik = t / To;
            yield return null;
        }
        t = 0;
        while (t < Back)
        {
            t += Time.deltaTime;
            _ik = 1 - t / Back;
            yield return null;
        }
        _ik = 0;
    }
    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (_animator)
        {

            //if the IK is active, set the position and rotation directly to the goal.
            if (ikActive)
            {

                // // Set the look target position, if one has been assigned
                // if (lookObj != null)
                // {
                //     animator.SetLookAtWeight(1);
                //     animator.SetLookAtPosition(lookObj.position);
                // }

                // Set the right hand target position and rotation, if one has been assigned
                // if (_ik != 0)
                // {
                _animator.SetIKPosition(AvatarIKGoal.RightHand, _shootTarget);
                _animator.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.LookRotation(transform.right));
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _ik);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, _ik);
                // }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                _animator.SetLookAtWeight(0);
            }
        }
    }
}
