using UnityEngine;

namespace gameCore
{
    internal class DefaultPlayerState : IState
    {
        private readonly PlayerController _player;
        public StateType Type { get; private set; } = StateType.DefaultState;
        public bool Equals(IState state) { return this.Type.Equals(state.Type); }
        public DefaultPlayerState(PlayerController player)
        {
            _player = player;
        }
        public void Enter()
        {
            Debug.Log("Entered " + getName());

        }
        public void Update()
        {

            if (PlayerActions.interractAction.WasPerformedThisFrame()) _player.processInterractAction();

            //if (PlayerActions.clickAction.WasPerformedThisFrame()) _player.processItemInterractAction();

            if (_player.getWasSelectedThisFrame())
            {
                _player.getInventory().unSelectItem();

                bool isItemSelected = false;

                if (PlayerActions.item1.WasPerformedThisFrame()) isItemSelected = _player.processSelectItemAction(0);
                else if (PlayerActions.item2.WasPerformedThisFrame()) isItemSelected = _player.processSelectItemAction(1);
                else if (PlayerActions.item3.WasPerformedThisFrame()) isItemSelected = _player.processSelectItemAction(2);
                else if (PlayerActions.item4.WasPerformedThisFrame()) isItemSelected = _player.processSelectItemAction(3);
                else if (PlayerActions.item5.WasPerformedThisFrame()) isItemSelected = _player.processSelectItemAction(4);
                else if (PlayerActions.item6.WasPerformedThisFrame()) isItemSelected = _player.processSelectItemAction(5);
                else if (PlayerActions.item7.WasPerformedThisFrame()) isItemSelected = _player.processSelectItemAction(6);
                else if (PlayerActions.item8.WasPerformedThisFrame()) isItemSelected = _player.processSelectItemAction(7);

                if (_player.getInventory().ActiveItem is StandingItem)
                    _player.stateMachine2.TransitionTo(_player.stateMachine2.States[StateType.PlacingState]);
            }

            //if (PlayerActions.dropAction.WasPerformedThisFrame()) _player.processDropAction();
            

        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

        }
        public string getName() { return Type.ToString(); }
    }
}


