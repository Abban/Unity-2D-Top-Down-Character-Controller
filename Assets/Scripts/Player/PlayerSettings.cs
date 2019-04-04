using UnityEngine;

namespace BBX.Player
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "PP/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Movement")]
        public float speed;
        
        [Header("Triggers")]
        public LayerMask triggerMask;
        
        [Header("Collision")]
        public LayerMask wallMask;
        [Range(0.001f, 0.3f)] public float skinWidth = 0.02f;
        [Range(2, 20)] public int horizontalRays = 8;
        [Range(2, 20)] public int verticalRays = 4;

        [Header("Items")]
        public string itemTag;
        public string containerTag;
    }
}