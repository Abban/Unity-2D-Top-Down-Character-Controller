using UnityEngine;
using Zenject;
using BBX.Player;

namespace BBX.Cameras
{
    public class CameraController : MonoBehaviour
    {
        private PlayerFacade _player;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player"></param>
        [Inject]
        public void Construct(PlayerFacade player)
        {
            _player = player;
        }
        
        
        /// <summary>
        /// runs once a frame
        /// </summary>
        private void LateUpdate()
        {
            if (_player == null) return;

            var playerPosition = _player.Position;
            
            transform.position = new Vector3(
                playerPosition.x,
                playerPosition.y,
                transform.position.z
            );
        }
    }
}