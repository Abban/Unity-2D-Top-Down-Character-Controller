using UnityEngine;

namespace BBX.Input.Interfaces
{
	public interface IInputState
	{
		/// <summary>
		/// Input on X-Axis
		/// </summary>
		float XAxis { get; set; }
		
		/// <summary>
		/// Input on Y-Axis
		/// </summary>
		float YAxis { get; set; }
		
		/// <summary>
		/// Enum with direction of input
		/// </summary>
		Vector2 CurrentDirection { get; }
		
		/// <summary>
		/// If the run button is held
		/// </summary>
		bool Collect { get; set; }
		
		/// <summary>
		/// If the jump button is held
		/// </summary>
		bool Search { get; set; }
		
		/// <summary>
		/// Call this to reset the states to default 
		/// </summary>
		void Reset();
	}
}