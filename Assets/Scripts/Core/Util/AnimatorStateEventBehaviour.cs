using System;
using UnityEngine;

public class AnimatorStateEventBehaviour : StateMachineBehaviour
{
    public event Action<int> StateEnter;
    public event Action<int> StateExit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        StateEnter?.Invoke(stateInfo.shortNameHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        StateExit?.Invoke(stateInfo.shortNameHash);
    }
}