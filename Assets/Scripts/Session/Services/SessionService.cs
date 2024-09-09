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
        [Inject] private readonly BlockService _blockService;

        [Inject] private readonly PadService _padService;
        [Inject] private readonly PadLaunchService _padLaunchService;
        [Inject] private readonly BallService _ballService;
        [Inject] private readonly BallSpeedService _ballSpeedService;
        
        [Inject] private readonly SessionUIService _sessionUIService;
        [Inject] private readonly ProgressService _progressService;
        
        private int _currentLevelIndex;
        private int _lifes;
        private bool _inProcess;
        private int _score;
        
        public event Action<int> LifeCountChanged;
        public event Action<int> ScoreChanged;
        public event Action<bool> InProcessChanged; 
        public int LifeCount => _lifes;
        public int Score => _score;
        public int CurrentLevelIndex => _currentLevelIndex;

        public void Start(int levelIndex)
        {
            _currentLevelIndex = levelIndex;
            _lifes = 3;

            Time.timeScale = 1f;
            
            _levelGenerator.Initialize();
            _levelGenerator.GenerateLevel(_levelCollection.Levels[_currentLevelIndex]);
            
            _padService.CreatePad();
            CreateNewBall();
            
            _sessionUIService.ShowGUIScreen();
            
            _inProcess = true;

            if (!_progressService.HelpShown)
            {
                _sessionUIService.ShowHelpScreen();
                _progressService.HelpShown = true;
                _progressService.Save();
            }
        }

        public void Retry()
        {
            Clear();
            Start(_currentLevelIndex);
        }

        public void Clear()
        {
            _levelGenerator.Dispose();
            _padService.DestroyPad();
            _ballService.DestroyAllBalls();
            _sessionUIService.HideGUIScreen();
        }
        
        public void Tick()
        {
            if (!_inProcess) return;
            UpdateLose();
            UpdateWin();
        }

        private void UpdateLose()
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

        private void UpdateWin()
        {
            if (_blockService.CurrentBlocksCount == 0)
            {
                Time.timeScale = 0f;
                _inProcess = false;
                _sessionUIService.ShowWinScreen();
                _progressService.Open(_currentLevelIndex + 1);
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