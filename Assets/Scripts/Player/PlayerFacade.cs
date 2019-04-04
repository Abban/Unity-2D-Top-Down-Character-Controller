using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using BBX.Player.Models;
using BBX.Library.FSM;
using BBX.Player.States;

namespace BBX.Player
{
    /// <summary>
    /// This is a general player controller that does the following:
    /// 1. Provides access to player functions to other systems
    /// 2. Ensures some of the sub components run in the correct order per frame
    /// 3. Runs a tiny FSM as an example of how that could work in Zenject
    /// </summary>
    public class PlayerFacade : MonoBehaviour
    {
        public Vector2 Position => _player.Position;
        
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
        [Inject]
        public void Construct(
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

        
        /// <summary>
        /// Initialise the state machine
        /// </summary>
        private void Start()
        {
            _stateMachine.ChangeState(_stateMap[States.Idle]);
        }

        
        /// <summary>
        /// Run update on the current state
        /// </summary>
        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }


        /// <summary>
        /// Check input and movement and swap state depending on what's going on
        /// </summary>
        private void LateUpdate()
        {
            _collisionHandler.Check(_player.Input.CurrentDirection);
            _movementHandler.Move(_player.Input.CurrentDirection);

            if (_player.IsMoving && _stateMachine.CurrentStateIs(_stateMap[States.Idle]))
            {
                _stateMachine.ChangeState(_stateMap[States.Moving]);
            }
            else if (!_player.IsMoving && _stateMachine.CurrentStateIs(_stateMap[States.Moving]))
            {
                _stateMachine.ChangeState(_stateMap[States.Idle]);
            }
        }
    }
}