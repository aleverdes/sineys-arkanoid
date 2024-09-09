using System;
using TaigaGames.SineysArkanoid.Session.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session.MonoBehaviours
{
    public class SessionFailScreen : MonoBehaviour
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private AudioClip _failSound;

        [Inject] private readonly SessionService _sessionService;
        [Inject] private readonly SessionUIService _sessionUIService;
        
        private void OnEnable()
        {
            _retryButton.onClick.AddListener(OnRetryButtonClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            
            AudioSource.PlayClipAtPoint(_failSound, Vector3.zero);
        }
        
        private void OnDisable()
        {
            _retryButton.onClick.RemoveListener(OnRetryButtonClicked);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnMainMenuButtonClicked();
        }
        
        private void OnRetryButtonClicked()
        {
            _sessionService.Retry();
            _sessionUIService.HideFailScreen();
        }
        
        private void OnMainMenuButtonClicked()
        {
            SceneManager.LoadScene(0);
        }
    }
}