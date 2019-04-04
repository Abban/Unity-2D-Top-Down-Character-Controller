using System;
using System.Collections.Generic;
using Zenject;
using BBX.Player.Models;
using BBX.Library.FSM;
using BBX.Player.States;

namespace BBX.Player
{
    /// <summary>
    /// This is a general parent controller that basically just exists to ensure that child
    /// controllers that have stuff that runs once a frame run in the correct order if needed
    /// </summary>
    public class PlayerController : IInitializable, ILateTickable
    {
        private PlayerModel _player;
        private PlayerCollisionHandler _collisionHandler;
        private PlayerMovementHandler _movementHandler;
        private StateMachine _stateMachine;

        private enum States
        {
            Idle,
            Moving
        }

        private readonly Dictionary<States, Type> _stateMap = new Dictionary<States, Type>
        {
            {States.Idle, typeof(PlayerIdleState)},
            {States.Moving, typeof(PlayerMovingState)}
        };


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player"></param>
        /// <param name="collisionHandler"></param>
        /// <param name="movementHandler"></param>
        /// <param name="stateMachine"></param>
        public PlayerController(
            PlayerModel player,
            PlayerCollisionHandler collisionHandler,
            PlayerMovementHandler movementHandler,
            StateMachine stateMachine)
        {
            _player = player;
            _collisionHandler = collisionHandler;
            _movementHandler = movementHandler;
            _stateMachine = stateMachine;
        }
        
        
        public void Initialize()
        {
            _stateMachine.ChangeState(_stateMap[States.Idle]);
        }
        
        
        /// <summary>
        /// Runs once a frame after all the Update/Tick
        /// </summary>
        public void LateTick()
        {
            _collisionHandler.Check(_player.Input.CurrentDirection);
            _movementHandler.Move(_player.Input.CurrentDirection);
        }

    }
}