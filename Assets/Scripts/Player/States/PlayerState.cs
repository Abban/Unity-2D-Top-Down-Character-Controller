using BBX.Library.FSM;
using BBX.Player.Models;
using Zenject;

namespace BBX.Player.States
{
    public class PlayerState : State
    {
        protected PlayerModel Player;
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player"></param>
        [Inject]
        public void Construct(PlayerModel player)
        {
            Player = player;
        }
        
        public override void Exit()
        {
        }
    }
}