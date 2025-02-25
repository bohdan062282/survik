using UnityEngine;

namespace gameCore
{
    internal class PlacingPlayerState : IState
    {
        private readonly PlayerController _player;
        public StateType Type { get; private set; } = StateType.PlacingState;
        public bool Equals(IState state) { return this.Type.Equals(state.Type); }
        public PlacingPlayerState(PlayerController player)
        {
            _player = player;
        }
        public void Enter()
        {
            Debug.Log("Entered " + getName());

        }
        public void Update()
        {
            



        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

        }
        public string getName() { return Type.ToString(); }
    }
}


