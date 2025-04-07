using UnityEngine;
using UnityEngine.Splines.Interpolators;

namespace gameCore
{
    internal class RunPlayerState:IState
    {

        private float _leftRightTransitionSpeed = 20.0f;
        private float _sprintTransitionSpeed = 20.0f;
        private float _forwardBackwardTransitionSpeed = 20.0f;

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

            if (!_player.characterController.isGrounded)
            {
                _player.fallingPlayerState.ForcesXZ = _player.inputVector * (PlayerActions.sprintAction.IsPressed() ?
                                                                                _player.sprintSpeed :
                                                                                _player.movementSpeed);

                _player.fallingPlayerState.PlayerForward = _player.transform.forward;
                _player.fallingPlayerState.PlayerRight = _player.transform.right;

                _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.FallingState]);
                _player.animator.SetBool(PlayerAnimationParams.isFalling, true);
            }
            else
            {
                if (_player.velocity.y < 0) _player.velocity.y = -2.0f;
                if (PlayerActions.jumpAction.WasPerformedThisFrame())
                {
                    _player.velocity.y = Mathf.Sqrt(_player.jumpHeight * 2f * _player.gravity);
                    _player.animator.SetBool(PlayerAnimationParams.isJump, true);
                    _player.animator.SetFloat(PlayerAnimationParams.runningJumpValue, 1.0f);
                }  
                else if (!_player.isWASD()) _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.IdleState]);
                else
                {
                    bool isSprint = PlayerActions.sprintAction.IsPressed() && _player.inputVector.x == 0 && _player.inputVector.y > 0;

                    _player.processMovement(isSprint ?
                                                _player.sprintSpeed :
                                                _player.movementSpeed, _player.inputVector.y, _player.inputVector.x);

                    if (isSprint) _player.animator.SetFloat(PlayerAnimationParams.sprintValue,
                                                            Mathf.Lerp( _player.animator.GetFloat(PlayerAnimationParams.sprintValue),
                                                                        1.0f, _sprintTransitionSpeed * Time.deltaTime));
                    else _player.animator.SetFloat(PlayerAnimationParams.sprintValue,
                                                   Mathf.Lerp(_player.animator.GetFloat(PlayerAnimationParams.sprintValue),
                                                   0.0f, _sprintTransitionSpeed * Time.deltaTime));

                    _player.animator.SetFloat(  PlayerAnimationParams.leftRightValue, 
                                                Mathf.Lerp( _player.animator.GetFloat(PlayerAnimationParams.leftRightValue),
                                                            _player.inputVector.x, _leftRightTransitionSpeed * Time.deltaTime));
                    
                    if (_player.inputVector.y >= 0)
                    {
                        _player.animator.SetFloat(PlayerAnimationParams.forwardBackwardValue,
                                                  Mathf.Lerp(_player.animator.GetFloat(PlayerAnimationParams.forwardBackwardValue),
                                                  1.0f, _forwardBackwardTransitionSpeed * Time.deltaTime));
                    }
                    else
                    {
                        _player.animator.SetFloat(PlayerAnimationParams.forwardBackwardValue,
                                                  Mathf.Lerp(_player.animator.GetFloat(PlayerAnimationParams.forwardBackwardValue),
                                                  -1.0f, _forwardBackwardTransitionSpeed * Time.deltaTime));
                    }

                }

            }


        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

            _player.animator.SetBool(PlayerAnimationParams.isMove, false);

        }
        public string getName() { return Type.ToString(); }
    }
}


