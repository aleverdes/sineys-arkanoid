using System;
using TaigaGames.SineysArkanoid.Session.Services;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session.Scenarios
{
    public class SessionGUIScenario : IInitializable, IDisposable
    {
        [Inject] private readonly SessionService _sessionService;
        [Inject] private readonly SessionUIService _sessionUIService;
        
        public void Initialize()
        {
            _sessionService.InProcessChanged += OnInProcessChanged;
            OnInProcessChanged(_sessionService.IsInProcess());
        }

        public void Dispose()
        {
            _sessionService.InProcessChanged -= OnInProcessChanged;
        }

        private void OnInProcessChanged(bool inProcess)
        {
            if (inProcess)
                _sessionUIService.ShowGUIScreen();
            else
                _sessionUIService.HideGUIScreen();
        }
    }
}