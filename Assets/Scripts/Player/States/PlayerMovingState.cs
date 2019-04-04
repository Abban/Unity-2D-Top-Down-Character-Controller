using Zenject;

namespace BBX.Player.States
{
    public class PlayerMovingState : PlayerState
    {
        public PlayerMovingState()
        {
            CanMoveToStates.Add(typeof(PlayerIdleState));
        }
        
        
        public class Factory : PlaceholderFactory<PlayerMovingState>
        {
        }
    }
}