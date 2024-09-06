using TaigaGames.SineysArkanoid.EntryPoint.Scenarios;
using Zenject;

namespace TaigaGames.SineysArkanoid.EntryPoint
{
    public class EntryPointInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntryPointScenario>().AsSingle();
        }
    }
}