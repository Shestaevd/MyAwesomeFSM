using Fsm.Fsm.Machine;
using Fsm.Machine;
using Fsm.State;
using System;

namespace Fsm.Fsm.Machine.Exended;

public static class StateMachineExtended
{
    public static State<T> NewStateInstance<T>(this StateMachine<T> manager, string name, ulong priority)
    {
        State<T> newState = new State<T>(name, priority);
        manager.AddState(newState);
        return newState;
    }
    public static State<T> NewStateInstance<T>(this StateMachine<T> manager, string name, ulong priority, Action<T> stateLogic)
    {
        State<T> newState = new State<T>(name, priority, stateLogic);
        manager.AddState(newState);
        return newState;
    }
    public static State<T> NewStateInstance<T>(this StateMachine<T> manager, string name, ulong priority, Action<T> stateLogic, Func<T, bool> enterCondition)
    {
        State<T> newState = new State<T>(name, priority, stateLogic, enterCondition);
        manager.AddState(newState);
        return newState;
    }
    public static State<T> NewStateInstance<T>(this StateMachine<T> manager, string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit)
    {
        State<T> newState = new State<T>(name, priority, stateLogic, onStateEnter, onStateExit);
        manager.AddState(newState);
        return newState;
    }
    public static State<T> NewStateInstance<T>(this StateMachine<T> manager, string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit, Func<T, bool> enterCondition)
    {
        State<T> newState = new State<T>(name, priority, stateLogic, onStateEnter, onStateExit, enterCondition);
        manager.AddState(newState);
        return newState;
    }
}
