using System;
using TaigaGames.SineysArkanoid.Ball.Services;
using TaigaGames.SineysArkanoid.Level.ScriptableObjects;
using TaigaGames.SineysArkanoid.Level.Services;
using TaigaGames.SineysArkanoid.Pad.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session.Services
{
    public class SessionService : ITickable
    {
        [Inject] private readonly LevelGenerator _levelGenerator;
        [Inject] private readonly LevelCollection _levelCollection;

        [Inject] private readonly PadService _padService;
        [Inject] private readonly PadLaunchService _padLaunchService;
        [Inject] private readonly BallService _ballService;
        [Inject] private readonly BallSpeedService _ballSpeedService;
        
        [Inject] private readonly SessionUIService _sessionUIService;
        
        private int _currentLevelIndex;
        private int _lifes;
        private bool _inProcess;
        private int _score;
        
        public event Action<int> LifeCountChanged;
        public event Action<int> ScoreChanged;
        public event Action<bool> InProcessChanged; 
        public int LifeCount => _lifes;
        public int Score => _score;

        public void Start(int levelIndex)
        {
            _currentLevelIndex = levelIndex;
            _lifes = 3;
            
            _levelGenerator.Initialize();
            _levelGenerator.GenerateLevel(_levelCollection.Levels[_currentLevelIndex]);
            
            _padService.CreatePad();
            CreateNewBall();
            
            _inProcess = true;
        }

        public void Tick()
        {
            if (!_inProcess) return;
            UpdateBalls();
        }

        private void UpdateBalls()
        {
            if (_ballService.GetBallsCount() > 0) return;
            
            if (_lifes > 0)
            {
                _lifes--;
                LifeCountChanged?.Invoke(_lifes);
                CreateNewBall();
            }
            else
            {
                _inProcess = false;
                _sessionUIService.ShowFailScreen();
            }
        }

        private void CreateNewBall()
        {
            _ballSpeedService.Reset();
            var newBall = _ballService.CreateBall(Vector2.zero, true);
            _padLaunchService.SetBallForLaunch(newBall);
        }
        
        public bool IsInProcess()
        {
            return _inProcess;
        }
        
        public void AddScore(int score)
        {
            _score += score;
            ScoreChanged?.Invoke(score);
        }
    }
}