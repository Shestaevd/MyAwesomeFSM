namespace Fsm.State
{
    public class State<T> : AbstractState<T>
    {
        private Action<T> _onEnter;
        private Action<T> _inState;
        private Action<T> _onExit;
        private Func<T, bool> _enterConditionD = _ => true;

        public State(string name, ulong priority)
        {
            Name = name;
            Priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic)
        {
            Name = name;
            _inState = stateLogic;
            Priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic, Func<T, bool> enterCondition)
        {
            Name = name;
            _inState = stateLogic;
            _enterConditionD = enterCondition;
            Priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit)
        {
            Name = name;
            _inState = stateLogic;
            _onEnter = onStateEnter;
            _onExit = onStateExit;
            Priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit, Func<T, bool> enterCondition)
        {
            Name = name;
            _inState = stateLogic;
            _onEnter = onStateEnter;
            _onExit = onStateExit;
            _enterConditionD = enterCondition;
            Priority = priority;
        }
        override protected void OnEnterLogic(T entity)
        {
            _onEnter?.Invoke(entity);
        }
        override protected void OnUpdateLogic(T entity)
        {
            _inState?.Invoke(entity);
        }
        override protected void OnExitLogic(T entity)
        {
            _onExit?.Invoke(entity);
        }
        override public bool EnterCondition(T entity)
        {
            if (_enterConditionD == null) return true;
            return _enterConditionD.Invoke(entity);
        }
        public State<T> SetName(string name)
        {
            Name = name;
            return this;
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
        public static State<T> GetEmpty()
        {
            return new State<T>("null_state", 0);
        }
    }
}
