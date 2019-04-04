using System;
using BBX.Library.FSM;

namespace BBX.Player.States
{
    public class PlayerStateFactory : IStateFactory
    {
        private PlayerIdleState.Factory _idleFactory;
        private PlayerMovingState.Factory _movingFactory;

        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idleFactory"></param>
        /// <param name="movingFactory"></param>
        public PlayerStateFactory(
            PlayerIdleState.Factory idleFactory,
            PlayerMovingState.Factory movingFactory)
        {
            _idleFactory = idleFactory;
            _movingFactory = movingFactory;
        }
        
        public IState Create(Type type)
        {
            if (type == typeof(PlayerIdleState))
            {
                return _idleFactory.Create();
            }
            
            if (type == typeof(PlayerMovingState))
            {
                return _movingFactory.Create();
            }
            
            throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}