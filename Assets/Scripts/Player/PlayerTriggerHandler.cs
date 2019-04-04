using BBX.Player.Models;
using UnityEngine;
using Zenject;

namespace BBX.Player
{
    public class PlayerTriggerHandler : MonoBehaviour
    {
        private PlayerModel _player;
        private PlayerSettings _playerSettings;

        [Inject]
        public void Construct(
            PlayerModel player,
            PlayerSettings playerSettings)
        {
            _player = player;
            _playerSettings = playerSettings;
        }


        /// <summary>
        /// When a player enters a trigger area
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_playerSettings.triggerMask.Contains(other.gameObject.layer)) return;
            
            _player.TriggerItems.AddItem(
                other.gameObject,
                Vector2.Distance(_player.Position, gameObject.transform.position)
            );
        }


        /// <summary>
        /// When a player hangs out in a trigger area
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!_playerSettings.triggerMask.Contains(other.gameObject.layer)) return;
            
            _player.TriggerItems.UpdateItem(
                other.gameObject,
                Vector2.Distance(_player.Position, gameObject.transform.position)
            );
        }


        /// <summary>
        /// When the player leaves the trigger area
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_playerSettings.triggerMask.Contains(other.gameObject.layer)) return;
            
            _player.TriggerItems.RemoveItem(other.gameObject);
        }
    }
}