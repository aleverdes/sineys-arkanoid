using System;
using TaigaGames.SineysArkanoid.Level.Services;
using TaigaGames.SineysArkanoid.Session.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session.MonoBehaviours
{
    public class SessionGUIScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform _lifeRoot;
        [SerializeField] private GameObject _lifePrefab;
        [SerializeField] private TMP_Text _infoText;
        
        [Inject] private readonly SessionService _sessionService;
        [Inject] private readonly BlockService _blockService;

        private void OnEnable()
        {
            _sessionService.LifeCountChanged += OnLifeCountChanged;
            OnLifeCountChanged(_sessionService.LifeCount);
        }
        
        private void OnDisable()
        {
            _sessionService.LifeCountChanged -= OnLifeCountChanged;
        }

        private void Update()
        {
            _infoText.text = "Level: " + (_sessionService.CurrentLevelIndex + 1) + "; Blocks: " + _blockService.CurrentBlocksCount + " / " + _blockService.StartBlocksCount;
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