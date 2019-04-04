using Zenject;

namespace BBX.Player.States
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState()
        {
            CanMoveToStates.Add(typeof(PlayerMovingState));
        }
        
        
        public class Factory : PlaceholderFactory<PlayerIdleState>
        {
        }
    }
}