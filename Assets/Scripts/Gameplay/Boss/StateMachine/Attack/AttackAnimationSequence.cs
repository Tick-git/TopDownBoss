using System.Collections.Generic;

public class AttackAnimationSequence
{
    private readonly Queue<AttackAnimationType> _animationSequence = new();

    public int Count => _animationSequence.Count;
    
    public AttackAnimationSequence AddStep(AttackAnimationType animationType)
    {
        _animationSequence.Enqueue(animationType);

        return this;
    }

    public AttackAnimationType GetNextStep()
    {
        return _animationSequence.Dequeue();
    }
}