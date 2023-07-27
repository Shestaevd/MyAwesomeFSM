using Fsm.State;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Fsm.Fsm;

public abstract class AbstractState<T>
{
    private List<StateModifier<T>> _Modifiers;

    public List<AbstractState<T>> BlackList { get; }
    public List<AbstractState<T>> WhiteList { get; }
    public List<AbstractState<T>> TransitFrom { get; }

    public bool Lock;

    public readonly string Name;
    public readonly ulong Priority;

    public AbstractState(string name, ulong priority)
    {
        _Modifiers = new List<StateModifier<T>>();

        Name = name;
        Priority = priority;
        BlackList = new List<AbstractState<T>>();
        WhiteList = new List<AbstractState<T>>();
        TransitFrom = new List<AbstractState<T>>();
    }

    public void AddModifier(StateModifier<T> m)
    {
        _Modifiers.Add(m);
    }
    
    public AbstractState<T> ToBlack(AbstractState<T> state)
    {
        BlackList.Add(state);
        return this;
    }
    public AbstractState<T> ToWhite(AbstractState<T> state)
    {
        WhiteList.Add(state);
        return this;
    }
    public AbstractState<T> ToTransitFrom(AbstractState<T> state)
    {
        TransitFrom.Add(state);
        return this;
    }

    internal bool EnterCondition(T entity)
    {
        return EnterCondition(entity);
    }
    internal void OnEnter(T entity)
    {
        OnEnterLogic(entity);
        _Modifiers.ForEach(m => m.EnterModify(entity));
    }
    internal void OnUpdate(T entity)
    {
        OnUpdateLogic(entity);
        _Modifiers.ForEach(m => m.UpdateModify(entity));
    }
    internal void OnExit(T entity)
    {
        OnExitLogic(entity);
        _Modifiers.ForEach(m => m.ExitModify(entity));
    }
    internal void OnEnterModifier(T entity)
    {
        _Modifiers.ForEach(modifier => modifier.EnterModify(entity));
    }
    internal void UpdateModifier(T entity)
    {
        _Modifiers.ForEach(modifier => modifier.UpdateModify(entity));
    }
    internal void OnExitModifier(T entity)
    {
        _Modifiers.ForEach(modifier => modifier.ExitModify(entity));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual bool OnEnterCondition(T entity)
    {
        return true;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract void OnEnterLogic(T entity);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract void OnUpdateLogic(T entity);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract void OnExitLogic(T entity);
}
