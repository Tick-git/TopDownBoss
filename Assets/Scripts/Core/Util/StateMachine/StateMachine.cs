using System.Collections.Generic;

public class StateMachine
{
    private StateNode _current;
    private StateNode _disabledState;

    private readonly HashSet<Transition> _anyTransitions = new();
    private readonly Dictionary<IState, StateNode> _nodes = new();

    private bool _enabled = true;

    public void Update()
    {
        if (!_enabled) return;
        
        if (TryGetTransition(out ITransition transition))
        {
            ChangeState(transition.TargetState);
        }

        _current?.State.Update();
    }

    public void FixedUpdate()
    {
        if (!_enabled) return;
        
        _current?.State.FixedUpdate();
    }

    public void AddTransition(IState from, IState to, IPredicate condition)
    {
        GetOrAddNode(from).Transitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }

    public void AddAnyTransition(IState to, IPredicate condition)
    {
        _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }

    public void SetState(IState state)
    {
        _current = GetOrAddNode(state);
        _current.State.Enter();
    }

    public void SetDisabledState(IState state)
    {
        _disabledState = GetOrAddNode(state);
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        _enabled = false;
        
        if (_disabledState != null)
        {
            _current?.State?.Exit();
            _current = _disabledState;
            _current.State.Enter();
        }
    }

    private void ChangeState(IState state)
    {
        IState previousSate = _current.State;
        IState nextSate = state;

        previousSate.Exit();
        nextSate.Enter();

        _current = _nodes[state];
    }

    private bool TryGetTransition(out ITransition nextTransition)
    {
        foreach (var transition in _anyTransitions)
        {
            if (transition.Condition.Evaluate())
            {
                nextTransition = transition;
                return true;
            }
        }

        foreach (var transition in _current.Transitions)
        {
            if (transition.Condition.Evaluate())
            {
                nextTransition = transition;
                return true;
            }
        }

        nextTransition = null;
        return false;
    }

    private StateNode GetOrAddNode(IState state)
    {
        if (!_nodes.TryGetValue(state, out StateNode node))
        {
            node = new StateNode(state);
            _nodes[state] = node;
        }

        return node;
    }

    class StateNode
    {
        public readonly IState State;
        public readonly HashSet<ITransition> Transitions;

        public StateNode(IState state)
        {
            State = state;
            Transitions = new HashSet<ITransition>();
        }

        public void AddTransition(IState to, IPredicate predicate)
        {
            Transitions.Add(new Transition(to, predicate));
        }
    }
}