using UnityEngine;

namespace gameCore
{
    internal class CombatPlayerState : IState
    {

        private float animationTransitionSpeed = 25.0f;

        private readonly PlayerController _player;
        public StateType Type { get; private set; } = StateType.CombatState;
        public bool Equals(IState state) { return this.Type.Equals(state.Type); }
        public CombatPlayerState(PlayerController player)
        {
            _player = player;
        }
        public void Enter()
        {
            Debug.Log("Entered " + getName());

        }
        public void Update()
        {
            _player.animator.SetLayerWeight(1, Mathf.Lerp(_player.animator.GetLayerWeight(1), 1.0f, animationTransitionSpeed * Time.deltaTime));

            _player.processRotation();


            if (!PlayerActions.rightClickAction.IsPressed())
            {
                _player.stateMachine2.TransitionTo(_player.stateMachine2.States[StateType.DefaultState]);
            }
            else if (PlayerActions.clickAction.WasPerformedThisFrame() && _player.combatScript.CanPunch)
            {
                _player.animator.SetBool(PlayerAnimationParams.isPunch, true);
                _player.combatScript.startPunch();
            }



        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

        }
        public string getName() { return Type.ToString(); }
    }
}
