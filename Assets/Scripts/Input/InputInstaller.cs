using BBX.Input.Interfaces;
using Zenject;

namespace BBX.Input
{
    public class InputInstaller : MonoInstaller<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputState>()
                .To<InputState>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<KeyboardHandler>()
                .AsSingle()
                .NonLazy();
        }
    }
}