using Fsm.Fsm;
using System;

namespace Fsm.State;

public class State<T> : AbstractState<T>
{
    private Action<T> _onEnter;
    private Action<T> _inState;
    private Action<T> _onExit;
    private Func<T, bool> _enterConditionD = _ => true;

    public State(string name, ulong priority) : base(name, priority)
    {
        
    }
    public State(string name, ulong priority, Action<T> stateLogic) : base(name, priority)
    {
        _inState = stateLogic;
    }
    public State(string name, ulong priority, Action<T> stateLogic, Func<T, bool> enterCondition) : base(name, priority)
    {
        _inState = stateLogic;
        _enterConditionD = enterCondition;
    }
    public State(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit) : base(name, priority)
    {
        _inState = stateLogic;
        _onEnter = onStateEnter;
        _onExit = onStateExit;
    }
    public State(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit, Func<T, bool> enterCondition) : base(name, priority)
    {
        _inState = stateLogic;
        _onEnter = onStateEnter;
        _onExit = onStateExit;
        _enterConditionD = enterCondition;
    }

    public static State<T> GetEmpty()
    {
        return new State<T>("null_state", 0);
    }

    public State<T> SetOnStateEnter(Action<T> enterLogic)
    {
        _onEnter = enterLogic;
        return this;
    }
    public State<T> SetStateLogic(Action<T> stateLogic)
    {
        _inState = stateLogic;
        return this;
    }
    public State<T> SetOnStateExit(Action<T> onStateExit)
    {
        _onExit = onStateExit;
        return this;
    }

    public new State<T> AddModifier(StateModifier<T> modifier)
    {
        base.AddModifier(modifier);
        return this;
    }

    public new State<T> ToBlack(AbstractState<T> state)
    {
        base.ToBlack(state);
        return this;
    }
    public new State<T> ToWhite(AbstractState<T> state)
    {
        base.ToWhite(state);
        return this;
    }
    public new State<T> ToTransitFrom(AbstractState<T> state)
    {
        base.ToTransitFrom(state);
        return this;
    }
    public State<T> SetEnterCondition(Func<T, bool> con)
    {
        _enterConditionD = con;
        return this;
    }

    protected override void OnEnterLogic(T entity)
    {
        _onEnter?.Invoke(entity);
    }
    protected override void OnUpdateLogic(T entity)
    {
        _inState?.Invoke(entity);
    }
    protected override void OnExitLogic(T entity)
    {
        _onExit?.Invoke(entity);
    }
    protected override bool EnterCondition(T entity)
    {
        if (_enterConditionD == null) return true;
        return _enterConditionD.Invoke(entity);
    }
}
