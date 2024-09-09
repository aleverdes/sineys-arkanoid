using TaigaGames.SineysArkanoid.Ball.ScriptableObjects;
using TaigaGames.SineysArkanoid.Pad.Services;
using TaigaGames.SineysArkanoid.Session.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Ball.Services
{
    public class BallSpeedService : ITickable
    {
        [Inject] private readonly BallSettings _ballSettings;
        [Inject] private readonly SessionService _sessionService;
        [Inject] private readonly PadLaunchService _padLaunchService;

        public float Speed { get; private set; } = 5f;

        public void Reset()
        {
            Speed = _ballSettings.BaseSpeed;
        }
        
        public void Tick()
        {
            if (!_sessionService.IsInProcess() || _padLaunchService.HasBallForLaunch())
                return;
            
            Speed = Mathf.Min(Speed + _ballSettings.IncreaseSpeedPerSecond * Time.deltaTime, _ballSettings.MaxSpeed);
        }
    }
}