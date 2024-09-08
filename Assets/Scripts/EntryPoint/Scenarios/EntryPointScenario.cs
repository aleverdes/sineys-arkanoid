using TaigaGames.SineysArkanoid.Session.Services;
using Zenject;

namespace TaigaGames.SineysArkanoid.EntryPoint.Scenarios
{
    public class EntryPointScenario : IInitializable
    {
        [Inject] private readonly SessionService _sessionService;
        
        public void Initialize()
        {
            _sessionService.Start(0);
        }
    }
}