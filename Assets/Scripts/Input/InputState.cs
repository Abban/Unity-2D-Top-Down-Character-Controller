using BBX.Input.Interfaces;
using UnityEngine;

namespace BBX.Input
{
    public class InputState : IInputState
    {
        public float XAxis { get; set; }
        public float YAxis { get; set; }
        public Vector2 CurrentDirection => new Vector2(XAxis, YAxis);
        public bool Collect { get; set; }
        public bool Search { get; set; }


        public void Reset()
        {
            XAxis = 0;
            YAxis = 0;
            Collect = false;
            Search = false;
        }
    }
}