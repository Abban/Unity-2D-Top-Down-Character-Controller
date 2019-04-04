using System;
using UnityEngine;
using Zenject;

namespace BBX.Player
{
    public class PlayerAnimationHandler : IFixedTickable
    {
        private Components _components;

        public PlayerAnimationHandler(Components components)
        {
            _components = components;
        }

        public void FixedTick()
        {
            throw new System.NotImplementedException();
        }
        
        [Serializable]
        public class Components
        {
            public Animator animator;
        }
    }
}