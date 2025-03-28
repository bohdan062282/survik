using UnityEngine;

namespace gameCore
{
    internal class FallingPlayerState:IState
    {
        private readonly PlayerController _player;
        public StateType Type { get; private set; } = StateType.FallingState;
        public bool Equals(IState state) { return this.Type.Equals(state.Type); }
        public FallingPlayerState(PlayerController player)
        {
            _player = player;
        }
        public void Enter()
        {
            Debug.Log("Entered " + getName());

        }
        public void Update()
        {
            
            
            if (!_player.characterController.isGrounded)
            {
                _player.velocity.y -= _player.gravity * Time.deltaTime;
                _player.processMovement(2.0f);
            }
            else
            {
                if (_player.isWASD()) _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.RunState]);
                else _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.IdleState]);
            }

      

        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

        }
        public string getName() { return Type.ToString(); }
    }
}


