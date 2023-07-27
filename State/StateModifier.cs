namespace Fsm.State;

public abstract class StateModifier<T>
{
    public abstract void UpdateModify(T entity);
    public abstract void EnterModify(T entity);
    public abstract void ExitModify(T entity);
}
