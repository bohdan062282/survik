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

                _player.processMovement(PlayerForward, PlayerRight, 0.7f, ForcesXZ.y, ForcesXZ.x);

            }
            else
            {
                if (_player.isWASD())
                {
                    _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.RunState]);
                    _player.animator.SetBool(PlayerAnimationParams.isMove, true);
                }
                else
                {
                    _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.IdleState]);
                }
            }

      

        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

            _player.animator.SetBool(PlayerAnimationParams.isFalling, false);
            _player.animator.SetBool(PlayerAnimationParams.isJump, false);

            ForcesXZ = Vector2.zero;
            PlayerForward = Vector3.zero;
            PlayerRight = Vector3.zero;

        }
        public string getName() { return Type.ToString(); }

        public Vector2 ForcesXZ { get; set; } = new Vector2(0.0f, 0.0f);
        public Vector3 PlayerForward { get; set; } = new Vector3(0.0f, 0.0f, 0.0f);
        public Vector3 PlayerRight { get; set; } = new Vector3(0.0f, 0.0f, 0.0f);
    }
}


