using TaigaGames.SineysArkanoid.Session.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session.MonoBehaviours
{
    public class SessionPauseScreen : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _mainMenuButton;
        
        [Inject] private readonly SessionUIService _sessionUIService;
        
        private void Awake()
        {
            Time.timeScale = 0f;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnContinueButtonClicked();
        }
        
        private void OnEnable()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }
        
        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }
        
        private void OnContinueButtonClicked()
        {
            _sessionUIService.HidePauseScreen();
        }
        
        private void OnMainMenuButtonClicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}