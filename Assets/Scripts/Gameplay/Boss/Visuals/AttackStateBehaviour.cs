using System;
using UnityEngine;

public class AttackStateBehaviour : StateMachineBehaviour
{
    [SerializeField] private AttackAnimationType _type;

    private bool _finished;

    public event Action<AttackAnimationType> AnimationFinished;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _finished = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_finished) return;

        if (stateInfo.normalizedTime >= 1f)
        {
            _finished = true;

            AnimationFinished?.Invoke(_type);
        }
    }
}