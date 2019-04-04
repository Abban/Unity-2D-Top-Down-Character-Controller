namespace BBX.Player.Models
{
    public class PlayerCollisionState
    {
        /// <summary>
        /// The player has a collision to the right
        /// </summary>
        public bool Right { get; set; }
        
        /// <summary>
        /// The player has a collision to the left
        /// </summary>
        public bool Left { get; set; }
        
        /// <summary>
        /// The player has a collision above
        /// </summary>
        public bool Above { get; set; }
        
        /// <summary>
        /// The player has a collision below
        /// </summary>
        public bool Below { get; set; }
        public float RaycastXPoint { get; set; }
        public float RaycastYPoint { get; set; }

        /// <summary>
        /// The player has a collision
        /// </summary>
        public bool HasCollision => Below || Right || Left || Above;
        
        /// <summary>
        /// The player has a collision on the x-axis
        /// </summary>
        public bool HasXCollision => Right || Left;
        
        /// <summary>
        /// The player has a collision on the y-axis
        /// </summary>
        public bool HasYCollision => Below || Above;
       
        
        public void Reset()
        {
            Right = false;
            Left = false;
            Above = false;
            Below = false;
            RaycastXPoint = 0f;
            RaycastYPoint = 0f;
        }


        public override string ToString()
        {
            return $"[CharacterCollisionState2D] r: {Right}, l: {Left}, a: {Above}, b: {Below}";
        }
    }
}