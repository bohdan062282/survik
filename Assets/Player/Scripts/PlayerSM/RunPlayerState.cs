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
            Debug.Log(_player.characterController.velocity.ToString() + " ; " + _player.velocity.ToString());

            if (!_player.characterController.isGrounded)
            {
                _player.fallingPlayerState.ForcesXZ = _player.inputVector * (PlayerActions.sprintAction.IsPressed() ?
                                                                                _player.sprintSpeed :
                                                                                _player.movementSpeed);

                _player.fallingPlayerState.PlayerForward = _player.transform.forward;
                _player.fallingPlayerState.PlayerRight = _player.transform.right;

                _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.FallingState]);
            }
            else
            {
                if (_player.velocity.y < 0) _player.velocity.y = -2.0f;
                if (PlayerActions.jumpAction.WasPerformedThisFrame())
                    _player.velocity.y = Mathf.Sqrt(_player.jumpHeight * 2f * _player.gravity);
                else if (!_player.isWASD()) _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.IdleState]);
                else _player.processMovement(PlayerActions.sprintAction.IsPressed() ?
                                                _player.sprintSpeed :
                                                _player.movementSpeed, _player.inputVector.y, _player.inputVector.x);

            }


        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

        }
        public string getName() { return Type.ToString(); }
    }
}


