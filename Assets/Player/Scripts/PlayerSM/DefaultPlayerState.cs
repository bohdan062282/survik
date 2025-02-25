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
            if (PlayerActions.clickAction.WasPerformedThisFrame()) _player.processItemInterractAction();
            else if (PlayerActions.dropAction.WasPerformedThisFrame()) _player.processDropAction();
            else if (PlayerActions.item1.WasPerformedThisFrame()) _player.processSelectItemAction(0);
            else if (PlayerActions.item2.WasPerformedThisFrame()) _player.processSelectItemAction(1);
            else if (PlayerActions.item3.WasPerformedThisFrame()) _player.processSelectItemAction(2);
            else if (PlayerActions.item4.WasPerformedThisFrame()) _player.processSelectItemAction(3);
            else if (PlayerActions.item5.WasPerformedThisFrame()) _player.processSelectItemAction(4);
            else if (PlayerActions.item6.WasPerformedThisFrame()) _player.processSelectItemAction(5);
            else if (PlayerActions.item7.WasPerformedThisFrame()) _player.processSelectItemAction(6);
            else if (PlayerActions.item8.WasPerformedThisFrame()) _player.processSelectItemAction(7);

        }
        public void Exit()
        {
            Debug.Log("Exit " + getName());

        }
        public string getName() { return Type.ToString(); }
    }
}


