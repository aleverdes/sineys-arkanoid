using TaigaGames.SineysArkanoid.Session.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session.Scenarios
{
    public class SessionPauseScenario : ITickable
    {
        [Inject] private readonly SessionUIService _sessionUIService;
        [Inject] private readonly SessionService _sessionService;
        
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _sessionService.IsInProcess())
            {
                if (_sessionUIService.IsPauseScreenActive())
                    _sessionUIService.HidePauseScreen();
                else
                    _sessionUIService.ShowPauseScreen();
            }
        }
    }
}