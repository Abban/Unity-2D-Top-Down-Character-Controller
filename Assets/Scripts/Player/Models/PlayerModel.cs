using System;
using BBX.Input.Interfaces;
using UnityEngine;

namespace BBX.Player.Models
{
    public class PlayerModel
    {
        private Components _components;
        
        public Transform Transform => _components.transform;
        public PlayerTriggerItems TriggerItems { get; }
        public PlayerCollisionState CollisionState { get; }
        public IInputState Input { get; }

        public Vector2 LastPosition { get; set; }

        public bool IsMoving => Vector2.Distance(Position, LastPosition) > 0.001;


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
            public Transform transform;
        }
    }
}