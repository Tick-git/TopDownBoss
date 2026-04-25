public class BaseState<T> : IState
{
    protected T Context { get;}

    public BaseState(T context)
    {
        Context = context;
    }

    public virtual void Enter()
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
        // NOOP
    }
}