using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayIdle()
    {
        _animator.Play("Idle");
    }

    public void PlayWalk()
    {
        _animator.Play("Walk");
    }

    public void SetLookDirection(bool looksLeft)
    {
        _spriteRenderer.flipX = looksLeft;
    }
}