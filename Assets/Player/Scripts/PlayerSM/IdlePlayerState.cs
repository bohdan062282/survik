
using UnityEngine;

namespace gameCore
{
    internal class IdlePlayerState:IState
    {
        private readonly PlayerController _player;
        public StateType Type { get; private set; } = StateType.IdlePlayerState;
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
            if (false)
            {

            }
            else if (_player.isWASD())
            {
                _player.stateMachine1.TransitionTo(_player.stateMachine1.States[StateType.RunPlayerState]);

            }



        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

        }
        public string getName() { return Type.ToString(); }
    }

}
