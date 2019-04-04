using BBX.Debugger;
using UnityEngine;
using Zenject;

namespace BBX.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public GameObject debugger;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.Bind<DebuggerInstaller>()
                .FromComponentInNewPrefab(debugger)
                .AsSingle()
                .NonLazy();
            
            Container.DeclareSignal<DebugMessageSignal>();
            Container.DeclareSignal<DebugClearSignal>();
        }
    }
}