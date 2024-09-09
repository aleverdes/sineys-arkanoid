using System;
using TaigaGames.SineysArkanoid.Level.Services;
using TaigaGames.SineysArkanoid.Session.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session.MonoBehaviours
{
    public class SessionGUIScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform _lifeRoot;
        [SerializeField] private GameObject _lifePrefab;
        [SerializeField] private TMP_Text _infoText;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private Button _pauseButton;
        
        [Inject] private readonly SessionService _sessionService;
        [Inject] private readonly SessionUIService _sessionUIService;
        [Inject] private readonly BlockService _blockService;

        private void OnEnable()
        {
            _sessionService.LifeCountChanged += OnLifeCountChanged;
            OnLifeCountChanged(_sessionService.LifeCount);
            
            _pauseButton.onClick.AddListener(OnPause);
        }
        
        private void OnDisable()
        {
            _sessionService.LifeCountChanged -= OnLifeCountChanged;
            _pauseButton.onClick.RemoveListener(OnPause);
        }

        private void OnPause()
        {
            _sessionUIService.ShowPauseScreen();
        }

        private void Update()
        {
            _infoText.text = "Уровень: " + (_sessionService.CurrentLevelIndex + 1) + " - Блоков: " + _blockService.CurrentBlocksCount + " / " + _blockService.StartBlocksCount;
            _progressSlider.value = 1f - _blockService.CurrentBlocksCount / (float)_blockService.StartBlocksCount;
        }

        private void OnLifeCountChanged(int lifeCount)
        {
            for (var i = _lifeRoot.childCount - 1; i >= 0; i--) 
                Destroy(_lifeRoot.GetChild(i).gameObject);
            for (var i = 0; i < lifeCount; i++) 
                Instantiate(_lifePrefab, _lifeRoot);
        }
    }
}