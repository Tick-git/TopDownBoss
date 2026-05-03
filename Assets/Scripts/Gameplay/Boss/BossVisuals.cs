using System;
using UnityEngine;

public class BossVisuals : MonoBehaviour
{
    Vector3 _rightLocalScale;
    private Vector3 _leftLocalScale;

    private void Awake()
    {
        _rightLocalScale = transform.localScale;
        _leftLocalScale = new Vector3(-_rightLocalScale.x, _rightLocalScale.y, _rightLocalScale.z);
    }

    public void Rotate(bool rotateRight)
    {
        transform.localScale = rotateRight ? _rightLocalScale : _leftLocalScale;
    }
}