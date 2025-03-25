

namespace gameCore
{
    internal interface IState
    {
        public void Enter();
        public void Update();
        public void Exit();
        public string getName();
        public bool Equals(IState state);
        public StateType Type { get; }
    }
    internal enum StateType { IdleState, RunState, DefaultState, PlacingState, ShootingState }
}
