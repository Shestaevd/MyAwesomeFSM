using Fsm.Fsm;
using Fsm.State;
using Fsm.Fsm.Machine;
using System.Collections.Generic;
using System;

namespace Fsm.Machine;

public class StateMachine<T>
{
    private List<AbstractState<T>> _States;

    public readonly T Entity;
    public AbstractState<T> CurrentState = State<T>.GetEmpty();

    public StateMachine(T entity)
    {
        Entity = entity;
        _States = new List<AbstractState<T>>();
    }
    public StateMachine(T entity, params AbstractState<T>[] states)
    {
        Entity = entity;
        new List<AbstractState<T>>(states).ForEach(s => AddState(s));
    }
    public StateMachine(T entity, List<AbstractState<T>> states)
    {
        Entity = entity;
        states.ForEach(s => AddState(s));
    }

    public void Run()
    {
        AbstractState<T> previous = CurrentState;
        if (GetNewState(ref CurrentState))
        {
            previous.OnExit(Entity);
            CurrentState.OnEnter(Entity);
            CurrentState.OnUpdate(Entity);
        }
        else
        {
            CurrentState.OnUpdate(Entity);
        }
    }

    public bool GetNewState(ref AbstractState<T> current)
    {
        if (!current.Lock)
        {
            ulong priority = 0;
            AbstractState<T> previous = current;
            switch (current.WhiteList.Count != 0 ? 1 : current.BlackList.Count != 0 ? -1 : 0)
            {
                case 1:
                    foreach (AbstractState<T> state in _States)
                        if (previous.WhiteList.Exists(s => s.Name == state.Name)
                            && (state.TransitFrom.Count == 0 || state.TransitFrom.Contains(previous))
                            && state.EnterCondition(Entity)
                            && priority < state.Priority)
                        {
                            priority = state.Priority;
                            current = state;
                        }
                    break;
                case -1:
                    foreach (AbstractState<T> state in _States)
                    {
                        if (previous.BlackList.TrueForAll(s => s.Name != state.Name)
                            && (state.TransitFrom.Count == 0 || state.TransitFrom.Contains(previous))
                            && state.EnterCondition(Entity)
                            && priority < state.Priority)
                        {
                            priority = state.Priority;
                            current = state;
                        }
                    }
                    break;
                case 0:
                    foreach (AbstractState<T> state in _States)
                    {
                        if (state.EnterCondition(Entity)
                            && (state.TransitFrom.Count == 0 || state.TransitFrom.Contains(previous))
                            && priority < state.Priority)
                        {
                            priority = state.Priority;
                            current = state;
                        }
                    }
                    break;
            }
            return previous.Name != current.Name;
        }
        else
        {
            return false;
        }
    }

    public StateMachine<T> AddState(AbstractState<T> newState)
    {
        if (_States.Exists(x => x.Name == newState.Name || x.Priority == newState.Priority))
        {
#if DEBUG
            Console.WriteLine($"State with name or priority: {newState.Name}:{newState.Priority} already exists");
#endif
            return this;
        }

        _States.Add(newState);
        return this;
    }
    public bool RemoveState(string name)
    {
        return _States.Remove(_States.Find(x => x.Name == name));
    }
}
