using Gameplay.Boss;

public abstract class BaseBossAttackState : IState
{
    protected BossController Context;
    public bool IsRunning { get; private set; }

    protected BaseBossAttackState(BossController context)
    {
        Context = context;
    }
    
    public virtual void Enter()
    {
        IsRunning = true;
        Context.AttackDecider.NotifyAttackStarted();
        Context.AttackSequenceRunner.AnimationChanged += OnAnimationEnter;
        Context.AttackSequenceRunner.SequenceFinished += OnSequenceFinished;
    }

    protected virtual void OnSequenceFinished()
    {
        CleanUp();
    }

    protected virtual void OnAnimationEnter(AttackAnimationType animationType)
    {
        // NOOP
    }


    public virtual void Update()
    {
        // NOOP
    }

    public virtual void FixedUpdate()
    {
        // NOOP
    }

    public virtual void Exit()
    {
        CleanUp();
        Context.AttackDecider.NotifyAttackEnded();
        Context.Animator.PlayIdle();
    }

    private void CleanUp()
    {
        IsRunning = false;
        Context.AttackSequenceRunner.AnimationChanged -= OnAnimationEnter;
        Context.AttackSequenceRunner.SequenceFinished -= OnSequenceFinished;
    }
}