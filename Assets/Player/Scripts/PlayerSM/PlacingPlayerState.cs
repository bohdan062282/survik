using UnityEngine;

namespace gameCore
{
    internal class PlacingPlayerState : IState
    {
        private StandingItem _standingItem;
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
            
            _standingItem = _player.getInventory().ActiveItem as StandingItem;
        }
        public void Update()
        {

            if (PlayerActions.interractAction.WasPerformedThisFrame()) _player.processInteractAction();

            if (PlayerActions.clickAction.WasPerformedThisFrame())
            {
                processStandingInteractAction();
            }
            else if (_player.getWasSelectedThisFrame())
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

                if (isItemSelected)
                {
                    if (_player.getInventory().ActiveItem is StandingItem)
                    {
                        if (_standingItem as Item != _player.getInventory().ActiveItem)
                            _player.stateMachine2.TransitionTo(_player.stateMachine2.States[StateType.PlacingState]);
                    }
                    else
                    {
                        _player.stateMachine2.TransitionTo(_player.stateMachine2.States[StateType.DefaultState]);
                    }
                }   
                    
            }
            else if (PlayerActions.dropAction.WasPerformedThisFrame())
            {
                _player.processDropAction();
                _player.stateMachine2.TransitionTo(_player.stateMachine2.States[StateType.DefaultState]);
            }


        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

        }
        public string getName() { return Type.ToString(); }
        private void processStandingInteractAction()
        {
            Item item = _player.getInventory().dropActiveItem();
            if (item != null)
            {
                _player.UIScript.unSetSelectedIcon();
                item.unSelect();

                item.interract();

                _player.UIScript.updateToolbar(_player.getInventory().Items);

            }
        }
    }
}


