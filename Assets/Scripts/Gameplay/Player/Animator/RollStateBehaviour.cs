using System;
using UnityEngine;

public class RollStateBehaviour : StateMachineBehaviour
{
    public event Action RollEnter;
    public event Action RollExit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        RollEnter?.Invoke();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        RollExit?.Invoke();
    }
}