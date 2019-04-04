using System;
using BBX.Input.Interfaces;
using UnityEngine;

namespace BBX.Player.Models
{
    public class PlayerModel
    {
        private Components _components;
        
        public Transform Transform => _components.transform;
        public Rigidbody2D Rigidbody2D => _components.rigidbody2D;
        public BoxCollider2D BoxCollider2D => _components.boxCollider2D;
        public PlayerTriggerItems TriggerItems { get; }
        public PlayerCollisionState CollisionState { get; }

        public IInputState Input { get; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="components"></param>
        /// <param name="triggerItems"></param>
        /// <param name="collisionState"></param>
        /// <param name="input"></param>
        public PlayerModel(
            Components components,
            PlayerTriggerItems triggerItems,
            PlayerCollisionState collisionState,
            IInputState input)
        {
            _components = components;
            TriggerItems = triggerItems;
            CollisionState = collisionState;
            Input = input;
        }
        
        
        /// <summary>
        /// Get or set position
        /// </summary>
        public Vector2 Position
        {
            get { return _components.transform.position; }
            set { _components.transform.position = value; }
        }
        
		
        [Serializable]
        public class Components
        {
            public Rigidbody2D rigidbody2D;
            public BoxCollider2D boxCollider2D;
            public Transform transform;
        }
    }
}