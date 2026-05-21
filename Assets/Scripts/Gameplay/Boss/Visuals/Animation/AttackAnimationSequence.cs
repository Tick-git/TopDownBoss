using System.Collections.Generic;

public class AttackAnimationSequence
{
    private readonly Queue<AttackAnimationStep> _animationSequence = new();

    public int Count => _animationSequence.Count;
    
    public AttackAnimationSequence AddStep(AttackAnimationType animationType, float animationTime = 0.25f)
    {
        _animationSequence.Enqueue(new AttackAnimationStep(animationType, animationTime));

        return this;
    }

    public AttackAnimationStep GetNextStep()
    {
        return _animationSequence.Dequeue();
    }
}

public readonly struct AttackAnimationStep
{
    public readonly AttackAnimationType AnimationType { get; }
    public readonly float AttackSpeedMultiplier { get; }
    
    public AttackAnimationStep(AttackAnimationType animationType, float attackSpeedMultiplier = 1.0f)
    {
        AnimationType = animationType;
        AttackSpeedMultiplier = attackSpeedMultiplier;
    }
}