

using System.Collections.Generic;

namespace gameCore
{
    internal class StateMachine
    {
        private IState _currentState;
        public IState CurrentState { get { return _currentState; } }
        public Dictionary<StateType, IState> States { get; private set; }
        public StateMachine(IState[] states)
        {
            States = new Dictionary<StateType, IState>();

            foreach (var state in states) States[state.Type] = state;

        }
        public void Initialize(IState startingState)
        {
            _currentState = startingState;
            startingState.Enter();
        }
        public void TransitionTo(IState nextState)
        {
            if (!this.Equals(nextState))
            {
                _currentState.Exit();
                _currentState = nextState;
                nextState.Enter();
            }
        }
        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.Update();
            }
        }
    }
}
