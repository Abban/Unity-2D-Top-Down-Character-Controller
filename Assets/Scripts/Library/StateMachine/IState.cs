using System;

namespace BBX.Library.FSM
{
	public interface IState
	{
		void Start(Type lastState);
		void Update();
		void Exit();
		bool CanMoveToState(IState state);
	}
}