using Zenject;
using UnityEngine;
using BBX.Input.Interfaces;
using BBX.Player.Models;
using BBX.Library.FSM;
using BBX.Player.States;

namespace BBX.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        // ReSharper disable once InconsistentNaming
        [Inject]
        public IInputState InputState;
        
        public PlayerSettings playerSettings;
        public PlayerModel.Components modelComponents;
        public PlayerGuiHandler.Components guiComponents;
        public PlayerAnimationHandler.Components animationComponents;
        public PlayerSoundHandler.Settings soundHandlerSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerSettings>()
                .FromInstance(playerSettings)
                .AsSingle();

            Container.Bind<PlayerModel>()
                .AsSingle()
                .WithArguments(modelComponents, InputState)
                .NonLazy();
            
            Container.BindInterfacesTo<PlayerController>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerMovementHandler>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerAnimationHandler>()
                .AsSingle()
                .WithArguments(animationComponents)
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlayerSoundHandler>()
                .AsSingle()
                .WithArguments(soundHandlerSettings)
                .NonLazy();
            
            Triggers();
            Collision();
            Gui();
            States();
        }

        
        /// <summary>
        /// Bind triggers
        /// </summary>
        private void Triggers()
        {
            Container.Bind<PlayerTriggerItems>()
                .AsSingle()
                .WhenInjectedInto<PlayerModel>();
                
            Container.BindFactory<string, float, GameObject, PlayerTriggerItems.Item, PlayerTriggerItems.Item.Factory>();
        }
    
        
        /// <summary>
        /// Bind collision
        /// </summary>
        private void Collision()
        {
            Container.Bind<PlayerCollisionState>()
                .AsSingle()
                .WhenInjectedInto<PlayerModel>();
            
            Container.BindInterfacesAndSelfTo<PlayerCollisionHandler>()
                .AsSingle()
                .NonLazy();
        }

        
        /// <summary>
        /// Bind GUI
        /// </summary>
        private void Gui()
        {
            Container.BindInterfacesAndSelfTo<PlayerGuiHandler>()
                .AsSingle()
                .WithArguments(guiComponents)
                .NonLazy();
        }

        
        /// <summary>
        /// Bind the player state machine and factories
        /// </summary>
        private void States()
        {
            Container.Bind<StateMachine>()
                .AsSingle()
                .WhenInjectedInto<PlayerController>();

            Container.Bind<IStateFactory>()
                .To<PlayerStateFactory>()
                .AsSingle()
                .WhenInjectedInto<StateMachine>();
            
            Container.BindFactory<PlayerIdleState, PlayerIdleState.Factory>();
            Container.BindFactory<PlayerMovingState, PlayerMovingState.Factory>();
        }
    }
}