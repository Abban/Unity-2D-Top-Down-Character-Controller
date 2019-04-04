using UnityEngine;
using BBX.Player.Models;

namespace BBX.Player
{
    public class PlayerMovementHandler
    {
        private PlayerModel _player;
        private PlayerSettings _playerSettings;
        
        private float SkinWidth => _playerSettings.skinWidth;
        private PlayerCollisionState CollisionState => _player.CollisionState;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player"></param>
        /// <param name="playerSettings"></param>
        public PlayerMovementHandler(
            PlayerModel player,
            PlayerSettings playerSettings)
        {
            _player = player;
            _playerSettings = playerSettings;
        }
        

        public void Move(Vector2 delta)
        {
            XMovement(ref delta);
            YMovement(ref delta);
            
            _player.Position += delta * _playerSettings.speed * Time.deltaTime;
        }
        
        
        private void XMovement(ref Vector2 delta)
        {
            if (!CollisionState.HasXCollision) return;
			
            var isGoingRight = delta.x > 0;
			
            // Colliding so force the delta to the collision point
            delta.x = CollisionState.RaycastXPoint;
			
            // Then add or subtract the skin depth
            if (isGoingRight)
            {
                delta.x -= SkinWidth;
            }
            else
            {
                delta.x += SkinWidth;
            }
        }
		
		
        private void YMovement(ref Vector2 delta)
        {
            if (!CollisionState.HasYCollision) return;
			
            var isGoingUp = delta.y > 0;
			
            // Colliding so force the delta to the collision point
            delta.y = CollisionState.RaycastYPoint;

            // Then add or subtract the skin depth
            if (isGoingUp)
            {
                delta.y -= SkinWidth;
            }
            else
            {
                delta.y += SkinWidth;
            }
        }
    }
}