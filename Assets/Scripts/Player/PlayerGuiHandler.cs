using System;
using BBX.Player.Models;
using UnityEngine;
using Zenject;

namespace BBX.Player
{
    public class PlayerGuiHandler : IInitializable, ILateTickable
    {
        private Components _components;
        private PlayerSettings _playerSettings;
        private PlayerModel _player;

        [Inject]
        public void Construct(
            Components components,
            PlayerSettings playerSettings,
            PlayerModel player)
        {
            _components = components;
            _playerSettings = playerSettings;
            _player = player;
        }


        public void Initialize()
        {
            _components.collectIcon.alpha = 0;
            _components.searchIcon.alpha = 0;
        }


        public void LateTick()
        {
            _components.collectIcon.alpha = _player.TriggerItems.ContainsTag(_playerSettings.itemTag) ? 1 : 0;
            _components.searchIcon.alpha = _player.TriggerItems.ContainsTag(_playerSettings.containerTag) ? 1 : 0;
        }


        [Serializable]
        public class Components
        {
            public CanvasGroup collectIcon;
            public CanvasGroup searchIcon;
        }
    }
}