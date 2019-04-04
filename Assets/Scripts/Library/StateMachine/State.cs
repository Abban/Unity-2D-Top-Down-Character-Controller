using System;
using System.Collections.Generic;

namespace BBX.Library.FSM
{
    public abstract class State : IState
    {
        protected readonly List<Type> CanMoveToStates = new List<Type>();
        protected Type FromState;
        
        
        /// <summary>
        /// When this state is entered
        /// </summary>
        /// <param name="lastState"></param>
        public virtual void Start(Type lastState)
        {
            FromState = lastState;
        }

        public virtual void Update()
        {
        }

        /// <summary>
        /// When this state is exited
        /// </summary>
        public abstract void Exit();
        
        
        /// <summary>
        /// Check if this state is allowed to move to another
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool CanMoveToState(IState state)
        {
            return CanMoveToStates.Contains(state.GetType());
        }
    }
}