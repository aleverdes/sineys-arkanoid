using TaigaGames.SineysArkanoid.MainMenu.MonoBehaviours;
using TaigaGames.SineysArkanoid.Session.Services;
using Zenject;

namespace TaigaGames.SineysArkanoid.EntryPoint.Scenarios
{
    public class EntryPointScenario : IInitializable
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly MainMenuScreen _mainMenuScreen;
        [Inject] private readonly ProgressService _progressService;
        
        public void Initialize()
        {
            _progressService.Load();
            _container.InstantiatePrefabForComponent<MainMenuScreen>(_mainMenuScreen);
        }
    }
}