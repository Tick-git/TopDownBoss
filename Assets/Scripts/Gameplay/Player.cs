using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private AssaultRifle _assaultRifle;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        Vector3 target = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        target.z = 0;
        
        _assaultRifle.ApplyAim(target);
        
        _spriteRenderer.flipX = target.x < transform.position.x;
    }
}
