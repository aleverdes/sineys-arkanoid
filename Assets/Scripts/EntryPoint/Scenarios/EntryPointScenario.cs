using TaigaGames.SineysArkanoid.Ball.Services;
using TaigaGames.SineysArkanoid.Level.ScriptableObjects;
using TaigaGames.SineysArkanoid.Level.Services;
using TaigaGames.SineysArkanoid.Pad.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.EntryPoint.Scenarios
{
    public class EntryPointScenario : IInitializable
    {
        [Inject] private readonly LevelGenerator _levelGenerator;
        [Inject] private readonly LevelCollection _levelCollection;

        [Inject] private readonly PadService _padService;
        [Inject] private readonly PadLaunchService _padLaunchService;
        [Inject] private readonly BallService _ballService;
        
        public void Initialize()
        {
            _levelGenerator.Initialize();
            _levelGenerator.GenerateLevel(_levelCollection.Levels[0]);
            
            _padService.CreatePad();
            
            var ball = _ballService.CreateBall(Vector2.zero, true);
            _padLaunchService.SetBallForLaunch(ball);
        }
    }
}