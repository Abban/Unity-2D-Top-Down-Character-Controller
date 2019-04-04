using UnityEngine;
using Zenject;
using BBX.Player;

namespace BBX.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public GameObject player;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerFacade>()
                .FromComponentInHierarchy(player)
                .AsSingle();
        }
    }
}