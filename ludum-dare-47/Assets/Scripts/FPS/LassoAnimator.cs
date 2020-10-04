using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoAnimator : MonoBehaviour
{
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void AnimationAttackFinished()
    {
        SendMessageUpwards("OnLassoAttackFinished", SendMessageOptions.DontRequireReceiver);
    }

    public void Attack()
    {
        _animator.SetTrigger("attack");
    }

    public void AttackFailed()
    {
        _animator.SetTrigger("idle");
    }
}