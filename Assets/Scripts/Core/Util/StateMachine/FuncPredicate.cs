using System;
using UnityEngine;

public class FuncPredicate : IPredicate
{
    private readonly Func<bool> _function;

    public FuncPredicate(Func<bool> function)
    {
        _function = function;
    }

    public bool Evaluate()
    {
        return _function();
    }
}