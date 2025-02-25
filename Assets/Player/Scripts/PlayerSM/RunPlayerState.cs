using UnityEngine;

namespace gameCore
{
    internal class RunPlayerState:IState
    {
        private readonly PlayerController _player;
        public StateType Type { get; private set; } = StateType.RunState;
        public bool Equals(IState state) { return this.Type.Equals(state.Type); }
        public RunPlayerState(PlayerController player)
        {
            _player = player;
        }
        public void Enter()
        {
            Debug.Log("Entered " + getName());

        }
        public void Update()
        {
            if (false)
            {

            }
            else
            {
                if (!_player.isWASD()) _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.IdleState]);
                else _player.processMovement();
            }


            

        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

        }
        public string getName() { return Type.ToString(); }
    }
}


