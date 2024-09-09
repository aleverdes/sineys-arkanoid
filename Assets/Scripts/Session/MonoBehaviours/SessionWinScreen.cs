using TaigaGames.SineysArkanoid.Session.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session.MonoBehaviours
{
    public class SessionWinScreen : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private AudioClip _winSound;
        
        [Inject] private readonly SessionService _sessionService;
        [Inject] private readonly SessionUIService _sessionUIService;
        [Inject] private readonly ProgressService _progressService;
        
        private void OnEnable()
        {
            _nextButton.onClick.AddListener(OnNextButtonClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            
            AudioSource.PlayClipAtPoint(_winSound, Vector3.zero);
        }
        
        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(OnNextButtonClicked);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnMainMenuButtonClicked();
        }

        private void OnNextButtonClicked()
        {
            _sessionService.Clear();
            _sessionService.Start(_progressService.CurrentLevelIndex);
            _sessionUIService.HideWinScreen();
        }
        
        private void OnMainMenuButtonClicked()
        {
            SceneManager.LoadScene(0);
        }
    }
}