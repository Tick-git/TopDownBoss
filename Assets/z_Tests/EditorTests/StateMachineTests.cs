using System;
using NUnit.Framework;

public class StateMachineTests
{
    private StateMachine _sm;

    [SetUp]
    public void Setup()
    {
        _sm = new StateMachine();
    }

    [Test]
    public void SetState_CallsEnter()
    {
        var state = new TestStateA();

        _sm.SetState(state);

        Assert.AreEqual(1, state.EnterCount);
    }

    [Test]
    public void Update_CallsStateUpdate()
    {
        var state = new TestStateA();

        _sm.SetState(state);
        _sm.Update();

        Assert.AreEqual(1, state.UpdateCount);
    }

    [Test]
    public void Transition_Happens_WhenConditionTrue()
    {
        var a = new TestStateA();
        var b = new TestStateB();

        bool condition = true;

        _sm.AddTransition(a, b, new FuncPredicate(() => condition));

        _sm.SetState(a);

        _sm.Update();

        Assert.AreEqual(1, a.ExitCount);
        Assert.AreEqual(1, b.EnterCount);
    }

    [Test]
    public void Transition_DoesNotHappen_WhenConditionFalse()
    {
        var a = new TestStateA();
        var b = new TestStateB();

        bool condition = false;

        _sm.AddTransition(a, b, new FuncPredicate(() => condition));

        _sm.SetState(a);

        _sm.Update();

        Assert.AreEqual(0, a.ExitCount);
        Assert.AreEqual(0, b.EnterCount);
    }

    [Test]
    public void AnyTransition_OverridesNormalTransitions()
    {
        var a = new TestStateA();
        var b = new TestStateB();
        var c = new TestStateC();

        _sm.AddTransition(a, b, new FuncPredicate(() => true));
        _sm.AddAnyTransition(c, new FuncPredicate(() => true));

        _sm.SetState(a);

        _sm.Update();

        Assert.AreEqual(1, a.ExitCount);
        Assert.AreEqual(1, c.EnterCount);
        Assert.AreEqual(0, b.EnterCount);
    }

    [Test]
    public void SameStateTransition_DoesNotReenter()
    {
        var a = new TestStateA();

        _sm.AddTransition(a, a, new FuncPredicate(() => true));

        _sm.SetState(a);

        _sm.Update();

        Assert.AreEqual(0, a.ExitCount);
        Assert.AreEqual(1, a.EnterCount);
    }
}

public abstract class TestBaseState : IState
{
    public int EnterCount;
    public int ExitCount;
    public int UpdateCount;

    public void Enter() => EnterCount++;
    public void Exit() => ExitCount++;
    public void Update() => UpdateCount++;

    public void FixedUpdate()
    {
    }
}

public class TestStateA : TestBaseState
{
}

public class TestStateB : TestBaseState
{
}

public class TestStateC : TestBaseState
{
}