using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void PlayIdle()
    {
        _animator.Play("Idle");
    }

    public void PlayWalk()
    {
        _animator.Play("Walk");
    }
}