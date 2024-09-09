using System;
using System.Collections.Generic;
using TaigaGames.SineysArkanoid.Level.ScriptableObjects;
using TaigaGames.SineysArkanoid.MainMenu.Types;
using TaigaGames.SineysArkanoid.Session.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TaigaGames.SineysArkanoid.MainMenu.MonoBehaviours
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private Button _quitButton;
        [SerializeField] private GridLayoutGroup _levelsGrid;
        [SerializeField] private LevelButton _levelsButtonPrefab;

        [Inject] private readonly LevelCollection _levelCollection;
        [Inject] private readonly ProgressService _progressService;
        [Inject] private readonly SessionService _sessionService;
        
        private readonly List<LevelButton> _levelButtons = new List<LevelButton>();
        
        [Inject]
        private void OnInitialize()
        {
            for (var i = 0; i < _levelCollection.Levels.Count; i++)
            {
                var button = Instantiate(_levelsButtonPrefab, _levelsGrid.transform);
                button.Setup(i, i == _progressService.CurrentLevelIndex ? LevelState.Current : i < _progressService.CurrentLevelIndex ? LevelState.Completed : LevelState.Locked);
                button.OnClick += OnLevelButtonClick;
                _levelButtons.Add(button);
            }
        }

        private void OnDestroy()
        {
            foreach (var button in _levelButtons)
                if (button)
                    button.OnClick -= OnLevelButtonClick;
        }

        private void OnEnable()
        {
            _quitButton.onClick.AddListener(OnQuitButtonClick);
        }
        
        private void OnDisable()
        {
            _quitButton.onClick.RemoveListener(OnQuitButtonClick);
        }

        private void OnQuitButtonClick()
        {
            Application.Quit();
        }

        private void OnLevelButtonClick(LevelButton levelButton)
        {
            if (levelButton.LevelIndex > _progressService.CurrentLevelIndex)
                return;
            
            Destroy(gameObject);
            _sessionService.Start(levelButton.LevelIndex);
        }
    }
}