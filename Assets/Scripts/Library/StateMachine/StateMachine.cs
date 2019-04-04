using System;
using UnityEngine;

namespace BBX.Library.FSM
{
	public class StateMachine
	{
		private IStateFactory _factory;
		public IState LastState { get; private set; }
		public IState CurrentState { get; private set; }

		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="factory"></param>
		public StateMachine(IStateFactory factory)
		{
			_factory = factory;
		}
		
		
		/// <summary>
		/// Try and change to a new state
		/// </summary>
		/// <param name="type"></param>
		public void ChangeState(Type type)
		{
			var state = _factory.Create(type);
			
			if (CurrentState == null)
			{
				CurrentState = state;
				CurrentState.Start(null);
				return;
			}
			
			if (!CurrentState.CanMoveToState(state))
			{
				Debug.LogWarningFormat(
					"{0} tried to move to {1}",
					CurrentState.GetType().Name,
					state.GetType().Name
				);
				return;
			}
			
			CurrentState.Exit();
			LastState = CurrentState;
			CurrentState = state;
			CurrentState.Start(LastState.GetType());
		}

		
		/// <summary>
		/// Check the current state against a type
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public bool CurrentStateIs(Type type)
		{
			return CurrentState.GetType() == type;
		}
		
		
		/// <summary>
		/// Check the last state against a type
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public bool LastStateWas(Type type)
		{
			return LastState.GetType() == type;
		}
	}
}