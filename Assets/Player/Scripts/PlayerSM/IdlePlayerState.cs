﻿
using UnityEngine;

namespace gameCore
{
    internal class IdlePlayerState:IState
    {
        private readonly PlayerController _player;
        public StateType Type { get; private set; } = StateType.IdleState;
        public bool Equals(IState state) { return this.Type.Equals(state.Type); }
        public IdlePlayerState(PlayerController player)
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
                _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.FallingState]);
                _player.animator.SetBool(PlayerAnimationParams.isFalling, true);
            }
            else
            {
                if (_player.velocity.y < 0) _player.velocity.y = -2.0f;
                if (PlayerActions.jumpAction.WasPerformedThisFrame())
                {
                    _player.velocity.y = Mathf.Sqrt(_player.jumpBaseHeight * 2f * _player.gravity);
                    _player.animator.SetBool(PlayerAnimationParams.isJump, true);
                    _player.animator.SetFloat(PlayerAnimationParams.runningJumpValue, 0.0f);
                }
                else if (_player.isWASD())
                {
                    _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.RunState]);
                    _player.animator.SetBool(PlayerAnimationParams.isMove, true);
                }

            }            


        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

        }
        public string getName() { return Type.ToString(); }
    }

}
