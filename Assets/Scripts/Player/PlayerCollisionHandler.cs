using System;
using UnityEngine;
using Zenject;
using BBX.Player.Models;

namespace BBX.Player
{
    /// <summary>
    /// Fires rays to detect a player's surroundings
    /// some of this is ripped from the Prime 2D Controller
    /// </summary>
    public class PlayerCollisionHandler : IInitializable
    {
        private struct CharacterRaycastOrigins
        {
            public Vector3 TopLeft;
            public Vector3 BottomRight;
            public Vector3 BottomLeft;
        }

        private PlayerModel _player;
        private Components _components;
        private PlayerSettings _playerSettings;
        private CharacterRaycastOrigins _raycastOrigins;
        private Vector2 _raySpacing;
        private const float SkinWidthFudgeFactor = 0.001f;
        
        private Vector2 LocalScale => _player.Transform.localScale;
        private PlayerCollisionState CollisionState => _player.CollisionState;
        private float SkinWidth => _playerSettings.skinWidth;
        private int HorizontalRays => _playerSettings.horizontalRays;
        private int VerticalRays => _playerSettings.verticalRays;
        private BoxCollider2D BoxCollider2D => _components.boxCollider2D;
        private LayerMask WallMask => _playerSettings.wallMask;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player"></param>
        /// <param name="components"></param>
        /// <param name="playerSettings"></param>
        public PlayerCollisionHandler(
            PlayerModel player,
            Components components,
            PlayerSettings playerSettings)
        {
            _player = player;
            _components = components;
            _playerSettings = playerSettings;
        }

        
        /// <summary>
        /// Initialise this object
        /// </summary>
        public void Initialize()
        {
            RecalculateDistanceBetweenRays();
        }

        
        /// <summary>
        /// Check for collisions
        /// </summary>
        /// <param name="delta"></param>
        public void Check(Vector2 delta)
        {
            CollisionState.Reset();
            
            ResetRaycastOrigins();

            if (Mathf.Abs(delta.x) > 0.001) CheckXAxis(delta);
            if (Mathf.Abs(delta.y) > 0.001) CheckYAxis(delta);
        }
        
        
        /// <summary>
        /// Check for collisions in the X-Axis
        /// </summary>
        /// <param name="delta"></param>
        private void CheckXAxis(Vector2 delta)
        {
            var isGoingRight = delta.x > 0;
            var rayDistance = Mathf.Abs(delta.x/2) + SkinWidth;
            var rayDirection = isGoingRight ? Vector2.right : Vector2.left;
            var initialRayOrigin = isGoingRight ? _raycastOrigins.BottomRight : _raycastOrigins.BottomLeft;

            for (var i = 0; i < HorizontalRays; i++)
            {
                var ray = new Vector2(initialRayOrigin.x, initialRayOrigin.y + i * _raySpacing.y);
                DrawRay(ray, rayDirection * rayDistance, Color.red);
                
                var raycastHit = Physics2D.Raycast(ray, rayDirection, rayDistance, WallMask);

                if (!raycastHit) continue;
                
                // Store the raycast hit point
                CollisionState.RaycastXPoint = raycastHit.point.x - ray.x;
                rayDistance = Mathf.Abs(CollisionState.RaycastXPoint);

                // Set collision states
                if (isGoingRight)
                {
                    CollisionState.Right = true;
                }
                else
                {
                    CollisionState.Left = true;
                }

                // we add a small fudge factor for the float operations here. if our rayDistance is smaller
                // than the width + fudge bail out because we have a direct impact
                if (rayDistance < SkinWidth + SkinWidthFudgeFactor) break;
            }
        }
        
        
        /// <summary>
        /// Check for collisions in the X-Axis
        /// </summary>
        /// <param name="delta"></param>
        private void CheckYAxis(Vector2 delta)
        {
            var isGoingUp = delta.y > 0;
            var rayDistance = Mathf.Abs(delta.y/2) + SkinWidth;
            
            var rayDirection = isGoingUp ? Vector2.up : -Vector2.up;
            var initialRayOrigin = isGoingUp ? _raycastOrigins.TopLeft : _raycastOrigins.BottomLeft;

            for (var i = 0; i < VerticalRays; i++)
            {
                var ray = new Vector2(initialRayOrigin.x + i * _raySpacing.x, initialRayOrigin.y);
                DrawRay(ray, rayDirection * rayDistance, Color.red);
                
                var raycastHit = Physics2D.Raycast(ray, rayDirection, rayDistance, WallMask);
                if (!raycastHit) continue;
                
                // Store the raycast hit point
                CollisionState.RaycastYPoint = raycastHit.point.y - ray.y;
                rayDistance = Mathf.Abs(CollisionState.RaycastYPoint);
                
                // Store the collision
                if (isGoingUp)
                {
                    CollisionState.Above = true;
                }
                else
                {
                    CollisionState.Below = true;
                }

                // we add a small fudge factor for the float operations here. if our rayDistance is smaller
                // than the width + fudge bail out because we have a direct impact
                if (rayDistance < SkinWidth + SkinWidthFudgeFactor) break;
            }
        }


        /// <summary>
        /// Reset the raycast origin positions to the current position of the character
        /// </summary>
        private void ResetRaycastOrigins()
        {
            // our raycasts need to be fired from the bounds inset by the skinWidth
            var modifiedBounds = BoxCollider2D.bounds;
            modifiedBounds.Expand(-2f * SkinWidth);

            _raycastOrigins.TopLeft = new Vector2(modifiedBounds.min.x, modifiedBounds.max.y);
            _raycastOrigins.BottomRight = new Vector2(modifiedBounds.max.x, modifiedBounds.min.y);
            _raycastOrigins.BottomLeft = modifiedBounds.min;
        }

        
        /// <summary>
        /// Draw a ray
        /// </summary>
        /// <param name="start"></param>
        /// <param name="dir"></param>
        /// <param name="color"></param>
        private static void DrawRay(Vector3 start, Vector3 dir, Color color)
        {
            Debug.DrawRay(start, dir, color);
        }


        /// <summary>
        /// This calculates the distance that needs to be between the rays being fired horizontally and vertically
        /// </summary>
        private void RecalculateDistanceBetweenRays()
        {
            var xSize = BoxCollider2D.size.x * Mathf.Abs(LocalScale.x) - 2f * SkinWidth;
            var ySize = BoxCollider2D.size.y * Mathf.Abs(LocalScale.y) - 2f * SkinWidth;
            
            _raySpacing = new Vector2(
                xSize / (VerticalRays - 1),
                ySize / (HorizontalRays - 1)
            );
        }

        
        [Serializable]
        public class Components
        {
            public BoxCollider2D boxCollider2D;
        }
    }
}