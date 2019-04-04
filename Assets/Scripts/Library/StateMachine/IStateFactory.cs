using System;

namespace BBX.Library.FSM
{
    public interface IStateFactory
    {
        /// <summary>
        /// Create a new state for the state machine
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IState Create(Type type);
    }
}