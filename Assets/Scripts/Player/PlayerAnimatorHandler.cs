using System;
using UnityEngine;
using Zenject;
using Spine.Unity;
using BBX.Player.Models;

namespace BBX.Player
{
    public class PlayerAnimatorHandler : IFixedTickable
    {
        public PlayerModel _player;
        public Components _components;

        private static readonly int YSpeed = Animator.StringToHash("ySpeed");

        private enum LookDirection
        {
            Right,
            Left
        }

        private LookDirection _lookDirection = LookDirection.Right;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player"></param>
        /// <param name="components"></param>
        public PlayerAnimatorHandler(
            PlayerModel player,
            Components components)
        {
            _player = player;
            _components = components;
        }


        /// <summary>
        /// If Player is shooting so always look towards that
        /// else if Player is moving then look in the moving direction
        /// </summary>
        public void FixedTick()
        {
            // Animator wasn't injected
            if (_components.animator == null) return;
            
            var xSpeed = _player.Position.x - _player.LastPosition.x;
            var ySpeed = _player.Position.y - _player.LastPosition.y;
            
            _components.animator.SetFloat(YSpeed, ySpeed);
            SetFlip(xSpeed);
            
            _player.LastPosition = _player.Position;
        }

        
        /// <summary>
        /// Flip the spine skeleton if needed
        /// </summary>
        /// <param name="xSpeed"></param>
        private void SetFlip(float xSpeed)
        {
            if (xSpeed < -0.01f && _lookDirection == LookDirection.Right)
            {
                _components.skeletonMecanim.skeleton.ScaleX = -1f;
                _lookDirection = LookDirection.Left;
            } else if (xSpeed > 0.01f && _lookDirection == LookDirection.Left)
            {
                _components.skeletonMecanim.skeleton.ScaleX = 1f;
                _lookDirection = LookDirection.Right;
            }
        }

        
        [Serializable]
        public class Components
        {
            public Animator animator;
            public SkeletonMecanim skeletonMecanim;
        }
    }
}