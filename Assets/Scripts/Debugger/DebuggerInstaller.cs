using UnityEngine;
using Zenject;

namespace BBX.Debugger
{
    public class DebuggerInstaller : MonoInstaller
    {
        public Debugger.Settings settings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Debugger>()
                .AsSingle()
                .WithArguments(settings)
                .NonLazy();
            
            Container.BindSignal<DebugMessageSignal>()
                .ToMethod<Debugger>(x => x.OnDebugMessage)
                .FromResolve();
            
            Container.BindSignal<DebugClearSignal>()
                .ToMethod<Debugger>(x => x.OnDebugClear)
                .FromResolve();
        }
    }
}