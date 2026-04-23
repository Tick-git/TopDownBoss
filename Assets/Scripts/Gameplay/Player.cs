using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private AssaultRifle _assaultRifle;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Animator _animator;
    
    private Camera _cam;
    private InputAction _moveAction;
    private InputAction _attackAction;

    private void Awake()
    {
        _cam = Camera.main;
        _moveAction = InputSystem.actions.FindAction("Move");
        _attackAction = InputSystem.actions.FindAction("Attack");
        _attackAction.performed += Attack;
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        _assaultRifle.Shoot();
    }

    private void Update()
    {
        Vector3 target = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        target.z = 0;

        Vector3 inputVector = _moveAction.ReadValue<Vector2>();
        
        transform.position += inputVector * (Time.deltaTime * _speed);

        if (inputVector != Vector3.zero)
        {
            _animator.Play("Walk");
        }
        else
        {
            _animator.Play("Idle");
        }
        
        _assaultRifle.ApplyAim(target);
        
        _spriteRenderer.flipX = target.x < transform.position.x;
    }
}
