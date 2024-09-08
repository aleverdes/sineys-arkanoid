﻿using TaigaGames.SineysArkanoid.Session.MonoBehaviours;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session.Services
{
    public class SessionUIService
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly SessionFailScreen _sessionFailScreenPrefab;
        [Inject] private readonly SessionPauseScreen _sessionPauseScreenPrefab;
        [Inject] private readonly SessionGUIScreen _sessionGUIScreenPrefab;

        private SessionFailScreen _sessionFailScreen;
        private SessionPauseScreen _sessionPauseScreen;
        private SessionGUIScreen _sessionGUIScreen;
        
        public void ShowFailScreen()
        {
            if (_sessionFailScreen) return;
            _sessionFailScreen = _diContainer.InstantiatePrefabForComponent<SessionFailScreen>(_sessionFailScreenPrefab);
        }

        public void ShowPauseScreen()
        {
            if (_sessionPauseScreen) return;
            _sessionPauseScreen = _diContainer.InstantiatePrefabForComponent<SessionPauseScreen>(_sessionPauseScreenPrefab);
        }
        
        public void HideFailScreen()
        {
            if (!_sessionFailScreen) return;
            Object.Destroy(_sessionFailScreen.gameObject);
            _sessionFailScreen = null;
        }
        
        public void HidePauseScreen()
        {
            if (!_sessionPauseScreen) return;
            Object.Destroy(_sessionPauseScreen.gameObject);
            _sessionPauseScreen = null;
        }
        
        public void ShowGUIScreen()
        {
            if (_sessionGUIScreen) return;
            _sessionGUIScreen = _diContainer.InstantiatePrefabForComponent<SessionGUIScreen>(_sessionGUIScreenPrefab);
        }
        
        public void HideGUIScreen()
        {
            if (!_sessionGUIScreen) return;
            Object.Destroy(_sessionGUIScreen.gameObject);
            _sessionGUIScreen = null;
        }

        public bool IsPauseScreenActive()
        {
            return _sessionPauseScreen;
        }
    }
}